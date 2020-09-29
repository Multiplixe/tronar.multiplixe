using Grpc.Net.Client;
using multiplixe.comum.enums;
using multiplixe.comum.helper.grpc;

namespace multiplixe.usuarios.client
{
    public class BaseClient
    {
        protected GrpcChannel channel { get; set; }

        public BaseClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.usuarios);
        }

    }
}
