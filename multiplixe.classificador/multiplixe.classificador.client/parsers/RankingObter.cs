using adduo.helper.extensionmethods;
using multiplixe.classificador.grpc.Protos;
using System;
using System.Net;
using coredto = multiplixe.comum.dto;
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

        public envelopes.ResponseEnvelope<coredto.ranking.Ranking> Response(RankingResponse response)
        {
            var envelopeResponse = new envelopes.ResponseEnvelope<coredto.ranking.Ranking>();
            envelopeResponse.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (envelopeResponse.Success)
            {
                envelopeResponse.Item = new coredto.ranking.Ranking
                {
                    DataProcessamento = new DateTime(response.Ranking.DataProcessamento)
                };

                foreach (var posicao in response.Ranking.Posicoes)
                {
                    envelopeResponse.Item.Posicoes.Add(new coredto.ranking.Posicao
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
