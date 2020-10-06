using multiplixe.classificador.grpc.Protos;
using comum_dto = multiplixe.comum.dto;
using envelope = adduo.helper.envelopes;

namespace multiplixe.classificador.client
{
    public class TransacaoClient : BaseClient
    {
        private Transacao.TransacaoClient client { get; set; }

        public TransacaoClient()
        {
            client = new Transacao.TransacaoClient(channel);
        }

        public envelope.ResponseEnvelope<comum_dto.externo.DebitoResponse> Debitar(comum_dto.externo.DebitoRequest debitoRequest)
        {
            var parser = new parsers.TransacaoDebitar();

            var request = parser.Request(debitoRequest);

            var debitoResponse = client.Debitar(request);

            var response = parser.Response(debitoResponse);

            return response;
        }

        public envelope.ResponseEnvelope<comum_dto.externo.EstornoResponse> Estornar(comum_dto.externo.EstornoRequest estornoRequest)
        {
            var parser = new parsers.TransacaoEstorno();

            var request = parser.Request(estornoRequest);

            var estornoResponse = client.Estornar(request);

            var response = parser.Response(estornoResponse);

            return response;
        }

    }
}
