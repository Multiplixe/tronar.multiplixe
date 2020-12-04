using adduo.helper.envelopes;
using multiplixe.comum.helper;
using multiplixe.facebook.autenticacao.dtos;
using multiplixe.usuarios.client;
using System;

namespace multiplixe.facebook.autenticacao
{
    public class IntegracaoUsuarioService
    {
        private readonly PerfilClient perfilClient;

        public IntegracaoUsuarioService(PerfilClient perfilClient)
        {
            this.perfilClient = perfilClient;
        }

        public void RegistrarPerfil(Guid usuarioId, Guid EmpresaId, comum.dto.PerfilAccessToken token, UserResponse userResponse)
        {
            var request = PreparaRequest(usuarioId, EmpresaId, token, userResponse);

            var response = perfilClient.Registrar(request);

            response.ThrownIfError();
        }

        private RequestEnvelope<comum.dto.Perfil> PreparaRequest(Guid usuarioId, Guid EmpresaId, comum.dto.PerfilAccessToken token, UserResponse userResponse)
        {
            var imagemUrl = userResponse.picture.data.url;

            var perfil = new comum.dto.Perfil
            {
                UsuarioId = usuarioId,
                EmpresaId = EmpresaId,
                PerfilId = userResponse.id,
                RedeSocial = comum.enums.RedeSocialEnum.facebook,
                Nome = userResponse.name,
                Login = userResponse.short_name,
                ExpiracaoToken = token.Expiracao,
                Token = token.Json,
                ImagemUrl = imagemUrl
            };

            return new RequestEnvelope<comum.dto.Perfil>(perfil);
        }

    }
}
