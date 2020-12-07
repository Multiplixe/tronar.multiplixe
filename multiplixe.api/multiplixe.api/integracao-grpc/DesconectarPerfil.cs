using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.usuarios.client;

namespace multiplixe.api.integracao_grpc
{
    public class DesconectarPerfil : IIntegracaoGRPC
    {
        private readonly Perfil perfil;

        public DesconectarPerfil(comum.dto.Perfil perfil)
        {
            this.perfil = perfil;
        }

        public ResponseEnvelope Enviar()
        {
            var client = new PerfilClient();

            return client.Desconectar(perfil.UsuarioId, perfil.PerfilId, perfil.RedeSocial, perfil.Ativo);
        }
    }
}
