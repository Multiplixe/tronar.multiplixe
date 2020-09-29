using multiplixe.classificador.grpc.Protos;
using dto = multiplixe.comum.dto;
using envelopes = adduo.helper.envelopes;

namespace multiplixe.classificador.grpc.parsers
{
    public class RankingObter
    {
        public RankingResponse Response(envelopes.ResponseEnvelope<dto.ranking.Ranking> envelope)
        {
            var response = new RankingResponse();

            response.HttpStatusCode = (int)envelope.HttpStatusCode;

            if (envelope.Success)
            {
                response.Ranking = new RankingMessage
                {
                    DataProcessamento = envelope.Item.DataProcessamento.Ticks
                };

                foreach (var posicao in envelope.Item.Posicoes)
                {
                    response.Ranking.Posicoes.Add(new PosicaoMessage
                    {
                        UsuarioId = posicao.UsuarioId.ToString(),
                        Pontos = posicao.Pontos,
                        Valor = posicao.Valor,
                        UsuarioAtual = posicao.UsuarioAtual
                    });
                }
            }

            return response;
        }
    }
}
