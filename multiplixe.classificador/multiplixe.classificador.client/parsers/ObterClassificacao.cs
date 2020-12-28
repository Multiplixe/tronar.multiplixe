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

        public ResponseEnvelope<comum_dto.classificacao.Classificacao> Response(ClassificacaoResponse response)
        {
            var envelope = new ResponseEnvelope<comum_dto.classificacao.Classificacao>
            {
                HttpStatusCode = (HttpStatusCode)response.HttpStatusCode
            };

            if (envelope.Success)
            {
                var nivel = response.Classificacao.Nivel;

                envelope.Item = new comum_dto.classificacao.Classificacao
                {
                    Pontuacao = new comum_dto.classificacao.Pontuacao
                    {
                        Valor = response.Classificacao.Pontuacao.Valor
                    },
                    Saldo = new comum_dto.classificacao.Saldo
                    {
                        Valor = response.Classificacao.Saldo.Valor
                    },
                    Nivel = new comum_dto.classificacao.Nivel
                    {
                        Anterior = new comum_dto.classificacao.NivelItem
                        {
                            Id = nivel.Anterior.Id,
                            Nome = nivel.Anterior.Nome,
                            Mostrar = nivel.Anterior.Mostrar
                        },
                        Atual = new comum_dto.classificacao.NivelItemAtual
                        {
                            Id = nivel.Atual.Nivel.Id,
                            Nome = nivel.Atual.Nivel.Nome,
                            Mostrar = nivel.Atual.Nivel.Mostrar,
                            PontosParaProximoNivel = nivel.Atual.PontosParaProximoNivel
                        },
                        Proximo = new comum_dto.classificacao.NivelItemProximo
                        {
                            Id = nivel.Proximo.Nivel.Id,
                            Nome = nivel.Proximo.Nivel.Nome,
                            Mostrar = nivel.Proximo.Nivel.Mostrar,
                            Pontos = nivel.Proximo.Pontos
                        }
                    }
                };

                foreach (var item in response.Classificacao.RedesSociais)
                {
                    envelope.Item.RedesSociais.Add(new comum_dto.classificacao.RedeSocial
                    {
                        Pontos = item.Pontos,
                        Percent = item.Percent,
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }
            }

            return envelope;
        }
    }
}
