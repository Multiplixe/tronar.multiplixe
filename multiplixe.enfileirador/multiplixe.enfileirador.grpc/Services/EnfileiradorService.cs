using Grpc.Core;
using multiplixe.enfileirador.grpc.Protos;
using System.Threading.Tasks;

namespace multiplixe.enfileirador.grpc.Services
{
    public class EnfileiradorService : Enfileirador.EnfileiradorBase
    {
        private RabbitMQService rabbitMQService { get; }

        public EnfileiradorService(RabbitMQService rabbitMQService)
        {
            this.rabbitMQService = rabbitMQService;
        }

        public override Task<ResponseMessage> Enfileirar(ItemMessage item, ServerCallContext context)
        {
            rabbitMQService.Enfileirar(item.NomeFila, item.Json);

            var response = new ResponseMessage { Ok = true };

            return Task.FromResult(response);
        }

    }
}
