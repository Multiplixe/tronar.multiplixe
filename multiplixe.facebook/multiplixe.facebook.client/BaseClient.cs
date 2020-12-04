using Grpc.Net.Client;
using multiplixe.comum.enums;
using multiplixe.comum.helper.grpc;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.facebook.client
{
    public class BaseClient
    {
        protected GrpcChannel channel { get; set; }

        public BaseClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.facebookGRPC);
        }

    }
}
