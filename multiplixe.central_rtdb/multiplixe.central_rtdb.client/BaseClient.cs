using multiplixe.comum.enums;
using multiplixe.comum.helper;
using multiplixe.comum.helper.grpc;
using Grpc.Net.Client;
using multiplixe.central_rtdb.grpc.protos;
using System.Net;

namespace multiplixe.central_rtdb.client
{
    public class BaseClient
    {
        private GrpcChannel channel { get; set; }
        protected RTDB.RTDBClient client { get; set; }

        public BaseClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.centralRTDB);

            client = new RTDB.RTDBClient(channel);
        }

        protected object ObterGatilho()
        {
            return new
            {
                timestamp = DateTimeHelper.Timestamp().ToString()
            };
        }



    }
}
