using multiplixe.classificador.grpc.Protos;
using Grpc;
using System.Threading.Tasks;
using multiplixe.classificador.ranking;
using System.Net;
using System;
using adduo.helper.extensionmethods;
using multiplixe.classificador.grpc.parsers;
using Grpc.Core;

namespace multiplixe.classificador.grpc.Services
{
    public class RankingService : Protos.Ranking.RankingBase
    {
        private Servico servico { get; }

        public RankingService(Servico servico)
        {
            this.servico = servico;
        }

        public override Task<RankingResponse> Obter(RankingRequest request, ServerCallContext context)
        {
            var response = new RankingResponse();

            try
            {
                var usuarioId = request.UsuarioId.ToGuid();

                var envelopeResponse = servico.Obter(usuarioId);

                var obterParser = new RankingObter();

                response = obterParser.Response(envelopeResponse);
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }
    }
}
