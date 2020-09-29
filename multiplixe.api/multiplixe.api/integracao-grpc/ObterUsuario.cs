using System;
using adduohelper = adduo.helper.envelopes;
using comum_dto =  multiplixe.comum.dto;
using usuario = multiplixe.usuarios.client;
namespace multiplixe.api.integracao_grpc
{
    public class ObterUsuario : IIntegracaoGRPC<comum_dto.Usuario>
    {
        private Guid usuarioId { get; }

        public ObterUsuario(Guid usuarioId)
        {
            this.usuarioId = usuarioId;
        }

        public adduohelper.ResponseEnvelope<comum_dto.Usuario> Enviar()
        {
            var usuarioClient = new usuario.UsuarioClient();

            var request = new adduohelper.RequestEnvelope<comum_dto.filtros.UsuarioFiltro>();
            request.Item = new comum_dto.filtros.UsuarioFiltro
            {
                UsuarioId = usuarioId
            };

            return usuarioClient.Obter(request);
        }
    }
}
