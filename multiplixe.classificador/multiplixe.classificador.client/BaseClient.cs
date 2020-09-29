using multiplixe.comum.enums;
using multiplixe.comum.helper.grpc;
using Grpc.Net.Client;

namespace multiplixe.classificador.client
{
    public class BaseClient
    {
        protected GrpcChannel channel { get; set; }

        public BaseClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.classificador);
        }

    }
}
