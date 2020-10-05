using multiplixe.classificador.client.parsers;
using multiplixe.classificador.grpc.Protos;
using System;
using comum_dto = multiplixe.comum.dto;
using coreexceptions = multiplixe.comum.exceptions;
using envelopes = adduo.helper.envelopes;

namespace multiplixe.classificador.client
{
    public class RankingClient : BaseClient
    {
        private Ranking.RankingClient client { get; set; }

        private RankingObter obterParser { get; set; }

        public RankingClient()
        {
            client = new Ranking.RankingClient(channel);

            obterParser = new RankingObter();
        }

        public envelopes.ResponseEnvelope<comum_dto.ranking.Ranking> Obter(Guid usuarioId)
        {
            var request = obterParser.Request(usuarioId);

            var response = client.Obter(request);

            var envelope = obterParser.Response(response);

            if (!envelope.Success)
            {
                throw new coreexceptions.GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }
    }
}
