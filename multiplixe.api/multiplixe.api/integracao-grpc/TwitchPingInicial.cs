using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.twitch.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace multiplixe.api.integracao_grpc
{
    public class TwitchPingInicial : IIntegracaoGRPC<comum.dto.TwitchPingResponse>
    {
        private readonly string twitchUserId;
        private readonly Guid empresaId;

        public TwitchPingInicial(string twitchUserId, Guid empresaId)
        {
            this.twitchUserId = twitchUserId;
            this.empresaId = empresaId;
        }

        public ResponseEnvelope<comum.dto.TwitchPingResponse> Enviar()
        {
            var client = new TwitchPingClient();

            return client.Inicial(twitchUserId, empresaId);
        }

        
    }
}
