using adduo.helper.envelopes;
using multiplixe.compartilhador.grpc.Protos;

namespace multiplixe.compartilhador.client
{
    public class PostClient : BaseClient
    {
        private Post.PostClient client { get; set; }

        public PostClient()
        {
            client = new Post.PostClient(channel);
        }

        public ResponseEnvelope<object> Obter(object o)
        {
            var parser = new parsers.PostObter();

            var request = parser.Request(o);

            var response = client.Obter(request);

            var envelope = parser.Response(response);

            return envelope;
        }

        public ResponseEnvelope ObterLista(object o)
        {
            var parser = new parsers.PostObterLista();

            var request = parser.Request(o);

            var response = client.ObterLista(request);

            var envelope = parser.Response(response);

            return envelope;
        }
    }
}
