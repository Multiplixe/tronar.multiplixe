using multiplixe.comum.helper;
using multiplixe.empresas.client;
using multiplixe.facebook.autenticacao.dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.facebook.autenticacao
{
    public class AtualizacaoAccessTokenService
    {
        private readonly EmpresaClient empresaClient;

        public AtualizacaoAccessTokenService(EmpresaClient empresaClient)
        {
            this.empresaClient = empresaClient;
        }

        public comum.dto.PerfilAccessToken Processar(string token, Guid empresaId)
        {
            var responseFacebookInfos = empresaClient.ObterInfoFacebook(empresaId);
            responseFacebookInfos.ThrownIfError();

            var facekookInfos = responseFacebookInfos.Item;

            var url = $"https://graph.facebook.com/{facekookInfos.GraphApiVersao}/oauth/access_token?grant_type=fb_exchange_token&client_id={facekookInfos.AppId}&client_secret={facekookInfos.AppSecret}&fb_exchange_token={token}";

            var envelopeResponse = WebRequestHelper.GetExterno<AccessTokenResponse>(url);
            envelopeResponse.ThrownIfError();

            var perfilToken = new comum.dto.PerfilAccessToken
            {
                Token = envelopeResponse.Item.access_token,
                Expiracao = DateTimeHelper.Now().AddSeconds(envelopeResponse.Item.expires_in),
                Tipo = envelopeResponse.Item.token_type
            };

            perfilToken.Json = SerializadorHelper.Serializar(perfilToken);

            return perfilToken;
        }
    }
}
