using adduo.helper.extensionmethods;
using multiplixe.classificador.grpc.Protos;
using System;
using System.Net;
using comum_dto = multiplixe.comum.dto;
using envelopes = adduo.helper.envelopes;

namespace multiplixe.classificador.client.parsers
{
    public class RankingObter
    {
        public RankingRequest Request(Guid usuarioId)
        {
            var request = new RankingRequest
            {
                UsuarioId = usuarioId.ToString()
            };

            return request;
        }

        public envelopes.ResponseEnvelope<comum_dto.ranking.Ranking> Response(RankingResponse response)
        {
            var envelopeResponse = new envelopes.ResponseEnvelope<comum_dto.ranking.Ranking>();
            envelopeResponse.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (envelopeResponse.Success)
            {
                envelopeResponse.Item = new comum_dto.ranking.Ranking
                {
                    DataProcessamento = new DateTime(response.Ranking.DataProcessamento)
                };

                foreach (var posicao in response.Ranking.Posicoes)
                {
                    envelopeResponse.Item.Posicoes.Add(new comum_dto.ranking.Posicao
                    {
                        UsuarioId = posicao.UsuarioId.ToGuid(),
                        Pontos = posicao.Pontos,
                        Valor = posicao.Valor,
                        UsuarioAtual = posicao.UsuarioAtual
                    });
                }
            }

            return envelopeResponse;
        }
    }
}
