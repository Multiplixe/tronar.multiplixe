using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.twitch.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace multiplixe.api.integracao_grpc
{
    public class TwitchPing : IIntegracaoGRPC<comum.dto.TwitchPingResponse>
    {
        private readonly string twitchUserId;
        private readonly string channelId;
        private readonly string is_Unlinked;
        private readonly string pingKeyHeader;
        private readonly string pingPausaHeader;
        private readonly Guid empresaId;

        public TwitchPing(
            string twitchUserId, 
            string channelId, 
            string is_unlinked, 
            string pingKeyHeader, 
            string pingPausaHeader, 
            Guid empresaId)
        {
            this.twitchUserId = twitchUserId;
            this.channelId = channelId;
            this.is_Unlinked = is_unlinked;
            this.pingKeyHeader = pingKeyHeader;
            this.pingPausaHeader = pingPausaHeader;
            this.empresaId = empresaId;
        }

        public ResponseEnvelope<comum.dto.TwitchPingResponse> Enviar()
        {
            var client = new TwitchPingClient();

            return client.Pingar(twitchUserId, channelId, is_Unlinked, pingKeyHeader, pingPausaHeader, empresaId);
        }

        
    }
}
