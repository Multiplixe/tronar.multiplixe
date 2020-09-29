using adduohelper = adduo.helper.envelopes;
using comum_dto =  multiplixe.comum.dto;
using usuario = multiplixe.usuarios.client;

namespace multiplixe.api.integracao_grpc
{
    public class RegistrarPerfil : IIntegracaoGRPC<comum_dto.Perfil>
    {
        private adduohelper.RequestEnvelope<comum_dto.Perfil> request { get; }

        public RegistrarPerfil(adduohelper.RequestEnvelope<comum_dto.Perfil> request)
        {
            this.request = request;
        }

        public adduohelper.ResponseEnvelope<comum_dto.Perfil> Enviar()
        {
            var perfilClient = new usuario.PerfilClient();
            return perfilClient.Registrar(request);
        }
    }
}
