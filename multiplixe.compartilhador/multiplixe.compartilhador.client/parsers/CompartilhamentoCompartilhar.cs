using adduo.helper.envelopes;
using multiplixe.compartilhador.grpc.Protos;
using comum_dto = multiplixe.comum.dto;


namespace multiplixe.compartilhador.client.parsers
{
    public class CompartilhamentoCompartilhar
    {
        public CompartilharRequest Request(comum_dto.Compartilhamento request)
        {
            return new CompartilharRequest()
            {
                PostId = request.PostId
            };
        }

        public ResponseEnvelope Response(BaseResponse response)
        {
            return new ResponseEnvelope();
        }

    }
}
