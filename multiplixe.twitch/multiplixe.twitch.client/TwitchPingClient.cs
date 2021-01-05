using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.twitch.grpc.Protos;
using System;

namespace multiplixe.twitch.client
{
    public class TwitchPingClient : BaseClient
    {
        private Ping.PingClient client { get; set; }

        public TwitchPingClient()
        {
            client = new Ping.PingClient(channel);
        }

        public ResponseEnvelope<TwitchPingResponse> Inicial(string twitchUserId, Guid empresaId)
        {
            var parser = new parsers.Iniciar();

            var request = parser.Request(twitchUserId, empresaId);

            var envelope = client.Inicial(request);

            var response = parser.Response(envelope);

            return response;
        }

        public ResponseEnvelope<TwitchPingResponse> Pingar(string twitchUserId, string channelId, string is_unlinked, string pingKeyHeader, string pingPausaHeader, Guid empresaId)
        {
            var parser = new parsers.Pingar();

            var request = parser.Request(twitchUserId, channelId, is_unlinked, pingKeyHeader, pingPausaHeader, empresaId);

            var envelope = client.Pingar(request);

            var response = parser.Response(envelope);

            return response;
        }
    }
}
