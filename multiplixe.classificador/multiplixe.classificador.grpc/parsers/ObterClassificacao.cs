using adduo.helper.extensionmethods;
using multiplixe.classificador.grpc.Protos;
using System.Net;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.grpc.parsers
{
    public class ObterClassificacao
    {
        public ClassificacaoResponse Response(dto.classificacao.Classificacao classificacao)
        {
            var response = new ClassificacaoResponse()
            {
                HttpStatusCode = (int)HttpStatusCode.OK
            };

            response.Classificacao = new ClassificacaoMessage
            {
                Pontuacao = new PontuacaoMessage
                {
                    Valor = classificacao.Pontuacao.Valor
                },
                Saldo = new SaldoMessage
                {
                    Valor = classificacao.Saldo.Valor
                },
                Nivel = new NivelMessage
                {
                    Anterior = new NivelItemMessage
                    {
                        Id = classificacao.Nivel.Anterior.Id,
                        Nome = classificacao.Nivel.Anterior.Nome.EmptyIfNull(),
                        Mostrar = classificacao.Nivel.Anterior.Mostrar
                    },
                    Atual = new NivelItemAtualMessage
                    {
                        Nivel = new NivelItemMessage
                        {
                            Id = classificacao.Nivel.Atual.Id,
                            Nome = classificacao.Nivel.Atual.Nome,
                            Mostrar = classificacao.Nivel.Atual.Mostrar
                        },
                        PontosParaProximoNivel = classificacao.Nivel.Atual.PontosParaProximoNivel
                    },
                    Proximo = new NivelItemProximoMessage
                    {
                        Nivel = new NivelItemMessage
                        {
                            Id = classificacao.Nivel.Proximo.Id,
                            Nome = classificacao.Nivel.Proximo.Nome.EmptyIfNull(),
                            Mostrar = classificacao.Nivel.Proximo.Mostrar
                        },
                        Pontos = classificacao.Nivel.Proximo.Pontos
                    },
                    Mudou = classificacao.Nivel.Mudou
                }
            };

            foreach (var item in classificacao.RedesSociais)
            {
                response.Classificacao.RedesSociais.Add(new RedeSocialMessage()
                {
                    Percent = item.Percent,
                    Pontos = item.Pontos,
                    Id = item.Id,
                    Nome = item.Nome
                });
            }

            return response;
        }
    }
}
