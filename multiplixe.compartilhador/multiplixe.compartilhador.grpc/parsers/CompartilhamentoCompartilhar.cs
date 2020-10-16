using multiplixe.compartilhador.grpc.Protos;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.compartilhador.grpc.parsers
{
    public class CompartilhamentoCompartilhar
    {
        public comum_dto.Compartilhamento Request(CompartilharRequest request)
        {
            return new comum_dto.Compartilhamento()
            {
                PostId = request.PostId
            };
        }

        public object Response(BaseResponse request)
        {
            return new object();
        }
    }
}
