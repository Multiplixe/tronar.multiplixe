using adduo.helper.envelopes;
using multiplixe.compartilhador.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.compartilhador.client.parsers
{
    public class PostObter
    {
        public ConsultaRequest Request(object o)
        {
            return new ConsultaRequest
            {
                EmpresaId = Guid.NewGuid().ToString(),
                UsuarioId = Guid.NewGuid().ToString(),
                PostId = string.Empty  // não remover isso, o GRPC dá erro com NULL
            };
        }

        public ResponseEnvelope<object> Response(ObterResponse response)
        {
            return new ResponseEnvelope<object>()
            {
                Item = new { Titulo = "Titulo do post" }
            };
        }
    }
}
