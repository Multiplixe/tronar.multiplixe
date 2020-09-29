using System;
using corehelper = multiplixe.comum.helper;
using protos = multiplixe.central_rtdb.grpc.protos;

namespace multiplixe.central_rtdb.client.parsers
{
    public class AtividadeComum
    {
        public protos.AtividadeRequest Request(string atividade, object o)
        {
            return new protos.AtividadeRequest
            {
                Nome = atividade,
                UsuarioId = string.Empty,
                Json = corehelper.SerializadorHelper.Serializar(o)
            };

        }

    }
}
