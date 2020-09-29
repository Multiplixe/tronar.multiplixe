using adduohelper = adduo.helper.envelopes;
using usuario = multiplixe.usuarios.client;
using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class AtualizarUsuario : IIntegracaoGRPC<comum_dto.entries.UsuarioAtualizacao>
    {
        private adduohelper.RequestEnvelope<comum_dto.entries.UsuarioAtualizacao> request { get; }

        public AtualizarUsuario(adduohelper.RequestEnvelope<comum_dto.entries.UsuarioAtualizacao> request)
        {
            this.request = request;
        }

        public adduohelper.ResponseEnvelope<comum_dto.entries.UsuarioAtualizacao> Enviar()
        {
            var usuarioClient = new usuario.UsuarioClient();
            return usuarioClient.Atualizar(request);
        }
    }
}
