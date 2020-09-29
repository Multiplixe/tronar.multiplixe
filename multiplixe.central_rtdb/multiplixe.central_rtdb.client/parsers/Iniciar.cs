using System;
using protos = multiplixe.central_rtdb.grpc.protos;

namespace multiplixe.central_rtdb.client.parsers
{
    class Iniciar
    {
        public protos.IniciarRequest Request(Guid usuarioId)
        {
            return new protos.IniciarRequest
            {
                UsuarioId = usuarioId.ToString()
            };

        }
    }
}
