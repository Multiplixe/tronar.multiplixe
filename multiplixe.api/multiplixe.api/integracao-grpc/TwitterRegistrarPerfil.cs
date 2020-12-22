using multiplixe.comum.dto;
using multiplixe.twitter.client;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.integracao_grpc
{
    public class TwitterRegistrarPerfil : IIntegracaoGRPC
    {
        private readonly TwitterOAuthResponse twitterOAuthResponse;

        public TwitterRegistrarPerfil(TwitterOAuthResponse twitterOAuthResponse)
        {
            this.twitterOAuthResponse = twitterOAuthResponse;
        }

        public adduohelper.ResponseEnvelope Enviar()
        {
            var client = new TwitterOAuthClient();
            return client.RegistrarPerfil(twitterOAuthResponse);
        }
    }
}
