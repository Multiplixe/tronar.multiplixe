using Grpc.Core;
using multiplixe.twitter.grpc.Protos;
using multiplixe.twitter.oauth;
using System;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.twitter.grpc.Services
{
    public class OAuthService : OAuth.OAuthBase
    {
        private readonly oauth.OAuthServico oauthServico;
        private readonly IntegracaoUsuario integracaoUsuario;

        public OAuthService(oauth.OAuthServico oauthServico, IntegracaoUsuario integracaoUsuario)
        {
            this.oauthServico = oauthServico;
            this.integracaoUsuario = integracaoUsuario;
        }

        public override async Task<RegistroResponse> RegistrarPerfil(RegistroRequest request, ServerCallContext context)
        {
            var response = new RegistroResponse();

            try
            {
                var usuarioId = Guid.Parse(request.UsuarioId);
                var empresaId = Guid.Parse(request.EmpresaId);

                var perfil = await oauthServico.ProcessarTokens(request.Token, request.Verifier, empresaId, request.ContaRedeSocial);

                perfil.UsuarioId = usuarioId;
                perfil.EmpresaId = empresaId;

                integracaoUsuario.Registrar(perfil);

                response.HttpStatusCode = (int)HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return response;
        }

        public override async Task<ObterURLResponse> ObterURL(ObterURLRequest request, ServerCallContext context)
        {
            var response = new ObterURLResponse();

            try
            {
                var empresaId = Guid.Parse(request.EmpresaId);

                var envelope = await oauthServico.Authenticate(empresaId, request.ContaRedeSocial);
                response.URL = envelope.Item;

                response.HttpStatusCode = (int)HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return response;
        }

    }
}
