using multiplixe.empresas.client;
using System;

namespace multiplixe.facebook.autenticacao
{
    public class Servico
    {
        private readonly AccessTokenService accessTokenService;
        private readonly UserService userService;
        private readonly IntegracaoUsuarioService integracaoUsuarioService;
        private readonly AtualizacaoAccessTokenService atualizacaoAccessTokenService;
        private readonly EmpresaClient empresaClient;

        public Servico(AccessTokenService accessTokenService, 
            UserService userService, 
            IntegracaoUsuarioService integracaoUsuarioService, 
            AtualizacaoAccessTokenService atualizacaoAccessTokenService,
            EmpresaClient empresaClient)
        {
            this.accessTokenService = accessTokenService;
            this.userService = userService;
            this.integracaoUsuarioService = integracaoUsuarioService;
            this.atualizacaoAccessTokenService = atualizacaoAccessTokenService;
            this.empresaClient = empresaClient;
        }

        public string ObterURLAutorizacao(Guid empresaId) 
        {
            var responseFacebookInfos = empresaClient.ObterInfoFacebook(empresaId);
            responseFacebookInfos.ThrownIfError();

            var facekookInfos = responseFacebookInfos.Item;

            return $"https://www.facebook.com/v9.0/dialog/oauth?client_id={facekookInfos.AppId}&redirect_uri={facekookInfos.URLRedirectOauth}&state=1";
        }

        public void ProcessarCode(string code, Guid usuarioId, Guid empresaId)
        {
            ValidarRequest(code);

            var token = accessTokenService.Obter(code, empresaId);

            var tokenLongaVida = atualizacaoAccessTokenService.Processar(token.access_token, empresaId);

            var user = userService.Obter(token.access_token);

            integracaoUsuarioService.RegistrarPerfil(usuarioId, empresaId, tokenLongaVida, user);
        }

        private static void ValidarRequest(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
        }
    }
}
