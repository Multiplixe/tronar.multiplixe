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
                Nivel = new NivelMessage
                {
                    Id = classificacao.Nivel.Id,
                    Nome = classificacao.Nivel.Nome,
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
