using adduo.helper.envelopes;
using multiplixe.compartilhador.grpc.Protos;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.compartilhador.client
{
    public class CompartilhamentoClient : BaseClient
    {
        private Compartilhamento.CompartilhamentoClient client { get; set; }

        public CompartilhamentoClient()
        {
            client = new Compartilhamento.CompartilhamentoClient(channel);
        }

        public ResponseEnvelope Compartilhar(comum_dto.Compartilhamento compartilhamento)
        {
            var parser = new parsers.CompartilhamentoCompartilhar();

            var request = parser.Request(compartilhamento);

            var response = client.Compartilhar(request);

            var envelope = parser.Response(response);

            return envelope;
        }

        public ResponseEnvelope Registrar(object o)
        {
            var parser = new parsers.CompartilhamentoRegistrar();

            var request = parser.Request(o);

            var response = client.Registrar(request);

            var envelope = parser.Response(response);

            return envelope;
        }
    }
}
