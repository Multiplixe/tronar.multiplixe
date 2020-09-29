using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using coredto = multiplixe.comum.dto;
using System.Net;

namespace multiplixe.classificador.client.parsers
{
    public class UsuarioSincronizar : UsuarioRegistrar
    {
        public override UsuarioRequest Request(RequestEnvelope<coredto.Usuario> request)
        {
            var usuarioMessage = new UsuarioRequest()
            {
                Id = request.Item.Id.ToString()
            };

            return usuarioMessage;
        }
       
    }
}
