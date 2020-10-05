using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using System;
using System.Net;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.classificador.client.parsers
{
    public class ObterClassificacao
    {
        public ClassificacaoRequest Request(Guid usuarioId)
        {
            return new ClassificacaoRequest { UsuarioId = usuarioId.ToString() };
        }

        public ResponseEnvelope<comum_dto.classificacao.Classificacao> Response(ClassificacaoResponse classificacaoResponse)
        {
            var responseEnvelope = new ResponseEnvelope<comum_dto.classificacao.Classificacao>
            {
                HttpStatusCode = (HttpStatusCode)classificacaoResponse.HttpStatusCode
            };

            if (responseEnvelope.Success)
            {
                responseEnvelope.Item = new comum_dto.classificacao.Classificacao
                {
                    Pontuacao = new comum_dto.classificacao.Pontuacao
                    {
                        Valor = classificacaoResponse.Classificacao.Pontuacao.Valor
                    },
                    Saldo = new comum_dto.classificacao.Saldo
                    {
                        Valor = classificacaoResponse.Classificacao.Saldo.Valor
                    },
                    Nivel = new comum_dto.classificacao.Nivel
                    {
                        Id = classificacaoResponse.Classificacao.Nivel.Id,
                        Nome = classificacaoResponse.Classificacao.Nivel.Nome,
                    }
                };

                foreach (var item in classificacaoResponse.Classificacao.RedesSociais)
                {
                    responseEnvelope.Item.RedesSociais.Add(new comum_dto.classificacao.RedeSocial
                    {
                        Pontos = item.Pontos,
                        Percent = item.Percent,
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }
            }

            return responseEnvelope;
        }
    }
}
