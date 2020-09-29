using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.registrador_de_eventos.client.twitter.parsers;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.twitter
{
    public class Client : BaseClient
    {
        private Twitter.TwitterClient client { get; set; }

        public Client()
        {
            client = new Twitter.TwitterClient(channel);
        }

        public void RegistrarReacao(Guid id, Guid usuarioId, string perfilId, string postId, DateTime dataEvento, object evento, TipoEventoEnum tipo)
        {
            var parser = new RegistrarReacao();

            var request = parser.Request(id, usuarioId, perfilId, postId, dataEvento, evento, tipo);

            client.RegistrarReacaoAsync(request)
              .GetAwaiter();
        }

        public ResponseEnvelope<Reacao> ObterUltimaReacao(Guid usuarioId, string postId)
        {
            var parser = new UltimaReacao();

            var request = parser.Request(usuarioId, postId);

            var response = client.UltimaReacao(request);

            var responseEnvelope = parser.Response(response);

            return responseEnvelope;
        }
    }
}
