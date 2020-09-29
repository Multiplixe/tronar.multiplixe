using adduohelper = adduo.helper.envelopes;
using usuario = multiplixe.usuarios.client;
using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class RegistrarUsuario : IIntegracaoGRPC<comum_dto.entries.UsuarioRegistro>
    {
        private adduohelper.RequestEnvelope<comum_dto.entries.UsuarioRegistro> request { get; }

        public RegistrarUsuario(adduohelper.RequestEnvelope<comum_dto.entries.UsuarioRegistro> request)
        {
            this.request = request;
        }

        public adduohelper.ResponseEnvelope<comum_dto.entries.UsuarioRegistro> Enviar()
        {
            var usuarioClient = new usuario.UsuarioClient();
            return usuarioClient.Registrar(request);
        }
    }
}
