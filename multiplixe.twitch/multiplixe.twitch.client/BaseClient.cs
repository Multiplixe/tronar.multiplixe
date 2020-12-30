using Grpc.Net.Client;
using multiplixe.comum.enums;
using multiplixe.comum.helper.grpc;
using System;

namespace multiplixe.twitch.client
{
    public class BaseClient
    {
        protected GrpcChannel channel { get; set; }

        public BaseClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.twitchGRPC);
        }

    }
}
