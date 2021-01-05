using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.twitch.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.twitch.client.parsers
{
    public class Ping
    {
        public TwitchPingResponse PingResponse(PingMessage message)
        {
            var response = new TwitchPingResponse
            {
                FrequenciaMinutos = message.FrequenciaMinutos,
                Chamada = message.Chamada
            };

            foreach (var item in message.DevolverHeader)
            {
                response.DevolverHeader[item.Key] = item.Value;
            }

            return response;

        }
    }
}
