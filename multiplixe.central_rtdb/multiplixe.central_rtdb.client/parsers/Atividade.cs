using System;
using corehelper = multiplixe.comum.helper;
using protos = multiplixe.central_rtdb.grpc.protos;

namespace multiplixe.central_rtdb.client.parsers
{
    public class Atividade
    {
        public protos.AtividadeRequest Request(Guid usuarioId, string atividade, object o)
        {
            return new protos.AtividadeRequest
            {
                Nome = atividade,
                UsuarioId = usuarioId.ToString(),
                Json = corehelper.SerializadorHelper.Serializar(o)
            };

        }

    }
}
