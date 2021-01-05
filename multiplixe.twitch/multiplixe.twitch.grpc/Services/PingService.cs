using Grpc.Core;
using multiplixe.comum.exceptions;
using multiplixe.twitch.grpc.Protos;
using System;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.twitch.grpc.Services
{
    public class PingService : Ping.PingBase
    {
        private readonly ping.PingService pingService;

        public PingService(
            ping.PingService pingService)
        {
            this.pingService = pingService;
        }

        public override Task<InicialResponse> Inicial(InicialRequest request, ServerCallContext context)
        {
            var response = new InicialResponse();

            try
            {
                var empresaId = Guid.Parse(request.EmpresaId);

                var envelope = pingService.Iniciar(request.TwitterUserId, empresaId);

                var parser = new parsers.Ping();

                response.Item = parser.Response(envelope);

                response.HttpStatusCode = (int)HttpStatusCode.Created;
            }
            catch (NotFoundException)
            {
                response.HttpStatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);
        }

        public override Task<PingarResponse> Pingar(PingarRequest request, ServerCallContext context)
        {
            var response = new PingarResponse();

            try
            {
                var empresaId = Guid.Parse(request.EmpresaId);

                var envelope = pingService.Pingar(request.TwitchUserId, request.ChannelId, request.IsUnlinked, request.PingKeyHeader, request.PingPausaHeader, empresaId);

                var parser = new parsers.Ping();

                response.Item = parser.Response(envelope);

                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);
        }
    }
}
