using multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.registrador_de_eventos.client.facebook.parsers;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;
using adduohelper = adduo.helper;

namespace multiplixe.registrador_de_eventos.client.facebook
{
    public class Client : BaseClient
    {
        private Facebook.FacebookClient client { get; set; }

        public Client()
        {
            client = new Facebook.FacebookClient(channel);
        }

        public void RegistrarReacao(Guid id, Guid usuarioId, string perfilId, string postId, DateTime dataEvento, object evento, string intensidade, TipoEventoEnum tipo)
        {
            var parser = new RegistrarReacao();

            var request = parser.Request(id, usuarioId, perfilId, postId, dataEvento, evento, intensidade, tipo);

            client.RegistrarReacaoAsync(request)
              .GetAwaiter();
        }

        public adduohelper.envelopes.ResponseEnvelope<Reacao> ObterUltimaReacao(Guid usuarioId, string postId)
        {
            var parser = new parsers.UltimaReacao();
            
            var request = parser.Request(usuarioId, postId);

            var response = client.UltimaReacaoAsync(request)
                                        .GetAwaiter()
                                        .GetResult();

            var responseEnvelope = parser.Response(response);

            return responseEnvelope;
        }
    }
}
