using multiplixe.usuarios.client;

namespace multiplixe.twitch.oauth
{
    public class IntegracaoUsuario
    {
        public void Registrar(comum.dto.Perfil perfil)
        {
            var client = new PerfilClient();
            var response = client.Registrar(new adduo.helper.envelopes.RequestEnvelope<comum.dto.Perfil>(perfil));
            response.ThrownIfError();
        }
    }
}
