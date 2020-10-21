using multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token.facebook
{
    public class ProcessadorToken : ServicoBase, IProcessarAccessToken
    {

        public ProcessadorToken(Guid empresaId, EmpresaClient empresaClient) : base(empresaId, empresaClient)
        {
        }

        /// <summary>
        /// Troca de token por um novo de longa duração
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public comum_dto.PerfilAccessToken TrocarToken(string json)
        {
            var accessToken = DeserializadorHelper.Deserializar<facebook.AccessToken>(json);
            var perfilAccessToken = Processar(accessToken.authResponse.accessToken);

            return perfilAccessToken;
        }

        private comum_dto.PerfilAccessToken Processar(string token)
        {
            var responseFacebookInfos = empresaClient.ObterInfoFacebook(empresaId);
            responseFacebookInfos.ThrownIfError();

            var facekookInfos = responseFacebookInfos.Item;

            var url = $"https://graph.facebook.com/{facekookInfos.GraphApiVersao}/oauth/access_token?grant_type=fb_exchange_token&client_id={facekookInfos.AppId}&client_secret={facekookInfos.AppSecret}&fb_exchange_token={token}";

            var envelopeResponse = WebRequestHelper.GetExterno<AccessTokenLongDuration>(url);
            envelopeResponse.ThrownIfError();

            var perfilToken = new comum_dto.PerfilAccessToken
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
