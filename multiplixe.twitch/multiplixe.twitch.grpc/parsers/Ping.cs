using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.twitch.grpc.Protos;

namespace multiplixe.twitch.grpc.parsers
{
    public class Ping
    {
        public PingMessage Response(ResponseEnvelope<TwitchPingResponse> envelope)
        {
            var response = new PingMessage()
            {
                FrequenciaMinutos = envelope.Item.FrequenciaMinutos,
                Chamada = envelope.Item.Chamada,
            };

            foreach (var item in envelope.Item.DevolverHeader)
            {
                response.DevolverHeader.Add(new DevolverHeaderItem
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }

            return response;
        }
    }
}
