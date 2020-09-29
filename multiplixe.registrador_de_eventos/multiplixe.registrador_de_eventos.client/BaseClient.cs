using multiplixe.comum.enums;
using multiplixe.comum.helper.grpc;
using Grpc.Net.Client;

namespace multiplixe.registrador_de_eventos.client
{
    public abstract class BaseClient
    {
        protected GrpcChannel channel { get; set; }

        public BaseClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.registradorDeEvento);
        }
    }
}
