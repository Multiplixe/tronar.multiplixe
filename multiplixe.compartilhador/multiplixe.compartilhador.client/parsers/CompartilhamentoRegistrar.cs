using adduo.helper.envelopes;
using multiplixe.compartilhador.grpc.Protos;

namespace multiplixe.compartilhador.client.parsers
{
    public class CompartilhamentoRegistrar
    {
        public RegistrarRequest Request(object request)
        {
            return new RegistrarRequest();
        }

        public ResponseEnvelope Response(BaseResponse response)
        {
            return new ResponseEnvelope();
        }

    }
}
