using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Grpc.Core;
using multiplixe.twitch.grpc.Protos;
using multiplixe.twitch.oauth;

namespace multiplixe.twitch.grpc.Services
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

                var perfil = await oauthServico.Processar(request.Code, empresaId, request.ContaRedeSocial);

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

        public override Task<ObterURLResponse> ObterURL(ObterURLRequest request, ServerCallContext context)
        {
            var response = new ObterURLResponse();

            try
            {
                var empresaId = Guid.Parse(request.EmpresaId);

                response.URL = oauthServico.Authenticate(empresaId, request.ContaRedeSocial);
                
                response.HttpStatusCode = (int)HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);
        }

    }
}
