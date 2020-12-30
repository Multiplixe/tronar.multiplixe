using multiplixe.comum.dto;
using multiplixe.twitch.client;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.integracao_grpc
{
    public class TwitchRegistrarPerfil : IIntegracaoGRPC
    {
        private readonly TwitchOAuthResponse twitchOAuthResponse;

        public TwitchRegistrarPerfil(TwitchOAuthResponse twitchOAuthResponse)
        {
            this.twitchOAuthResponse = twitchOAuthResponse;
        }

        public adduohelper.ResponseEnvelope Enviar()
        {
            var client = new TwitchOAuthClient();
            return client.RegistrarPerfil(twitchOAuthResponse);
        }
    }
}
