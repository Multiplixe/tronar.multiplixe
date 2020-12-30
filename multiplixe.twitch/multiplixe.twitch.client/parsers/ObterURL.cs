using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.twitch.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.twitch.client.parsers
{
    public class ObterURL
    {
        public ObterURLRequest Request(Guid empresaId, string contaRedeSocial)
        {
            return new ObterURLRequest
            {
                EmpresaId = empresaId.ToStringEmptyIfEmpty(),
                ContaRedeSocial = contaRedeSocial
            };
        }

        public ResponseEnvelope<string> Response(ObterURLResponse response)
        {
            var envelope = new ResponseEnvelope<string>(string.Empty);

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (envelope.Success)
            {
                envelope.Item = response.URL;
            }
            else
            {
                envelope.Error.Messages.Add(response.Erro);
            }

            return envelope;
        }
    }
}
