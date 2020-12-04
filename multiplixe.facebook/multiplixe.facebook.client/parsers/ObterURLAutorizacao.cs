using adduo.helper.envelopes;
using multiplixe.facebook.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Net;

namespace multiplixe.facebook.client.parsers
{
    public class ObterURLAutorizacao
    {
        public ObterURLAutorizacaoRequest Request(Guid empresaId)
        {
            return new ObterURLAutorizacaoRequest { EmpresaId = empresaId.ToString() };
        }

        public ResponseEnvelope<string> Response(ObterURLAutorizacaoResponse response)
        {
            var envelope = new ResponseEnvelope<string>(string.Empty)
            {
                HttpStatusCode = (HttpStatusCode)response.HttpStatusCode
            };

            if (envelope.Success)
            {
                envelope.Item = response.URL;
            }
            else
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
