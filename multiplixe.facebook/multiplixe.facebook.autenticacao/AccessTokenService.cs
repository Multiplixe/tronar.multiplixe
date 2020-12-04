using multiplixe.comum.helper;
using multiplixe.empresas.client;
using multiplixe.facebook.autenticacao.dtos;
using System;
using System.Security.Authentication;

namespace multiplixe.facebook.autenticacao
{
    public class AccessTokenService
    {
        private readonly EmpresaClient empresaClient;

        public AccessTokenService(EmpresaClient empresaClient)
        {
            this.empresaClient = empresaClient;
        }

        public AccessTokenResponse Obter(string code, Guid empresaId)
        {
            var responseFacebookInfos = empresaClient.ObterInfoFacebook(empresaId);
            responseFacebookInfos.ThrownIfError();

            var facekookInfos = responseFacebookInfos.Item;

            var url = $"https://graph.facebook.com/v9.0/oauth/access_token?client_id={facekookInfos.AppId}&redirect_uri={facekookInfos.URLRedirectOauth}&state=1&client_secret={facekookInfos.AppSecret}&code={code}";

            var envelope = WebRequestHelper.GetExterno<AccessTokenResponse>(url);

            var accessTokenResponse = envelope.Item;

            if (accessTokenResponse.error != null &&
                accessTokenResponse.error.code == 100)
            {
                if (accessTokenResponse.error.error_subcode == 36007)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    throw new InvalidCredentialException();
                }
            }

            return accessTokenResponse;
        }
    }
}
