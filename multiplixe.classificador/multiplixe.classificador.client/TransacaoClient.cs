using multiplixe.classificador.grpc.Protos;
using System;
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
    }
}
