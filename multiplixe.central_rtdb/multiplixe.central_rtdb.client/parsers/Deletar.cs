using System;
using protos = multiplixe.central_rtdb.grpc.protos;

namespace multiplixe.central_rtdb.client.parsers
{
    class Deletar
    {
        public protos.DeletarRequest Request(Guid usuarioId)
        {
            return new protos.DeletarRequest
            {
                UsuarioId = usuarioId.ToString()
            };

        }
    }
}
