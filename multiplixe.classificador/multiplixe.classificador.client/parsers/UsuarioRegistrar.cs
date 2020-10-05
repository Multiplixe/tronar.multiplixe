using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using comum_dto = multiplixe.comum.dto;
using System.Net;

namespace multiplixe.classificador.client.parsers
{
    public class UsuarioRegistrar
    {
        public virtual UsuarioRequest Request(RequestEnvelope<comum_dto.Usuario> request)
        {
            var usuarioMessage = new UsuarioRequest()
            {
                Id = request.Item.Id.ToString(),
                EmpresaId = request.Item.EmpresaId.ToString()
            };

            return usuarioMessage;
        }

        public ResponseEnvelope Response(UsuarioResponse usuarioResponse)
        {
            var responseEnvelope = new ResponseEnvelope()
            {
                HttpStatusCode = (HttpStatusCode)usuarioResponse.HttpStatusCode
            };

            return responseEnvelope;
        }

    }
}
