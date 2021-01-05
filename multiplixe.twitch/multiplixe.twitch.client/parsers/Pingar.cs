using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.comum.dto;
using multiplixe.twitch.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.twitch.client.parsers
{
    public class Pingar : Ping
    {
        public PingarRequest Request(string twitchUserId, string channelId, string is_unlinked, string pingKeyHeader, string pingPausaHeader, Guid empresaId)
        {
            return new PingarRequest
            {
                TwitchUserId = twitchUserId,
                ChannelId = channelId,
                IsUnlinked = is_unlinked,
                PingKeyHeader = pingKeyHeader,
                PingPausaHeader = pingPausaHeader,
                EmpresaId = empresaId.ToStringEmptyIfEmpty()
            };
        }

        public ResponseEnvelope<TwitchPingResponse> Response(PingarResponse response)
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
