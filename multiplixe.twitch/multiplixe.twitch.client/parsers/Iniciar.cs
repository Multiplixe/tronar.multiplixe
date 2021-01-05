using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.comum.dto;
using multiplixe.twitch.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.twitch.client.parsers
{
    public class Iniciar : Ping
    {
        public InicialRequest Request(string twitchUserId, Guid empresaId)
        {
            return new InicialRequest
            {
                TwitterUserId = twitchUserId,
                EmpresaId = empresaId.ToStringEmptyIfEmpty()
            };
        }


        public ResponseEnvelope<TwitchPingResponse> Response(InicialResponse response)
        {
            var envelope = new ResponseEnvelope<TwitchPingResponse>();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (envelope.Success)
            {
                envelope.Item = PingResponse(response.Item);
            }
            else
            {
                envelope.Error.Messages.Add(response.Erro);
            }

            return envelope;

        }
    }
}
