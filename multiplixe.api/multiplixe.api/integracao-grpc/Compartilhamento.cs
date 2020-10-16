using adduo.helper.envelopes;
using multiplixe.compartilhador.client;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class Compartilhamento : IIntegracaoGRPC
    {

        private adduohelper.RequestEnvelope<comum_dto.Compartilhamento> request { get; }

        public Compartilhamento(adduohelper.RequestEnvelope<comum_dto.Compartilhamento> request)
        {
            this.request = request;
        }

        public ResponseEnvelope Enviar()
        {
            var grpc = new CompartilhamentoClient();

            return grpc.Compartilhar(request.Item);

        }
    }
}
