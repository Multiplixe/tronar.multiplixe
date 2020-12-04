using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.facebook.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace multiplixe.facebook.client.parsers
{
    public class ProcessarCode
    {
        public ProcessarCodeRequest Request(string code, Guid usuarioId, Guid empresaId)
        {
            return new ProcessarCodeRequest { Code = code.EmptyIfNull(), UsuarioId = usuarioId.ToString(), EmpresaId = empresaId.ToString() };
        }

        public ResponseEnvelope Response(ProcessarCodeResponse response)
        {
            var envelope = new ResponseEnvelope
            {
                HttpStatusCode = (HttpStatusCode)response.HttpStatusCode
            };

            if (!envelope.Success)
            {
                envelope.Error = new ErrorEnvelope
                {
                    Exception = new Exception(response.Erro),
                    Messages = new List<string> { response.Erro }
                };
            }

            return envelope;
        }
    }
}
