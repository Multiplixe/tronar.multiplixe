using adduo.helper.envelopes;
using multiplixe.classificador.client;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class TransacaoEstorno : IIntegracaoGRPC<comum_dto.externo.EstornoResponse>
    {
        private comum_dto.externo.EstornoRequest request { get; }

        public TransacaoEstorno(comum_dto.externo.EstornoRequest request)
        {
            this.request = request;
        }

        public ResponseEnvelope<comum_dto.externo.EstornoResponse> Enviar()
        {
            var grpc = new TransacaoClient();

            return grpc.Estornar(request);
        }
    }
}
