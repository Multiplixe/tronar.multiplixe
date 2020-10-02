using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using System;
using System.Net;
using coredto = multiplixe.comum.dto;

namespace multiplixe.classificador.client.parsers
{
    public class ObterClassificacao
    {
        public ClassificacaoRequest Request(Guid usuarioId)
        {
            return new ClassificacaoRequest { UsuarioId = usuarioId.ToString() };
        }

        public ResponseEnvelope<coredto.classificacao.Classificacao> Response(ClassificacaoResponse classificacaoResponse)
        {
            var responseEnvelope = new ResponseEnvelope<coredto.classificacao.Classificacao>
            {
                HttpStatusCode = (HttpStatusCode)classificacaoResponse.HttpStatusCode
            };

            if (responseEnvelope.Success)
            {
                responseEnvelope.Item = new coredto.classificacao.Classificacao
                {
                    Pontuacao = new coredto.classificacao.Pontuacao
                    {
                        Valor = classificacaoResponse.Classificacao.Pontuacao.Valor
                    },
                    Saldo = new coredto.classificacao.Saldo
                    {
                        Valor = classificacaoResponse.Classificacao.Saldo.Valor
                    },
                    Nivel = new coredto.classificacao.Nivel
                    {
                        Id = classificacaoResponse.Classificacao.Nivel.Id,
                        Nome = classificacaoResponse.Classificacao.Nivel.Nome,
                    }
                };

                foreach (var item in classificacaoResponse.Classificacao.RedesSociais)
                {
                    responseEnvelope.Item.RedesSociais.Add(new coredto.classificacao.RedeSocial
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
