using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.twitter.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.twitter.client.parsers
{
    public class ProcessarCRC
    {
        public CRCRequest Request(string crc, Guid empresaId, string contaRedeSocial)
        {
            return new CRCRequest
            {
                CRC = crc,
                EmpresaId = empresaId.ToStringEmptyIfEmpty(),
                ContaRedeSocial = contaRedeSocial
            };
        }

        public ResponseEnvelope<comum.dto.TwitterCRCResponse> Response(CRCResponse response) 
        {
            var envelope = new ResponseEnvelope<comum.dto.TwitterCRCResponse>();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (envelope.Success)
            {
                envelope.Item.response_token = response.ResponseToken;
            }
            else
            {
                envelope.Error.Messages.Add(response.Erro);
            }

            return envelope;
        }
    }
}
