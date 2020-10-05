using adduo.helper.envelopes;
using multiplixe.classificador.client;
using multiplixe.comum.dto.externo;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class TransacaoDebitar : IIntegracaoGRPC<comum_dto.externo.DebitoResponse>
    {
        private DebitoRequest request { get; }

        public TransacaoDebitar(DebitoRequest request)
        {
            this.request = request;
        }

        public ResponseEnvelope<DebitoResponse> Enviar()
        {
            var grpc = new TransacaoClient();
            return grpc.Debitar(request);
        }
    }
}
