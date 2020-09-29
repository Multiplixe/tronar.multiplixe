using multiplixe.classificador.grpc.Protos;
using System.Net;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.grpc.parsers
{
    public class ObterPontuacaoTotal
    {
        public PontuacaoResponse Response(dto.classificacao.Pontuacao pontuacao)
        {
            var response = new PontuacaoResponse()
            {
                HttpStatusCode = (int)HttpStatusCode.OK
            };

            response.Pontuacao = new PontuacaoMessage
            {
                Valor = pontuacao.Valor
            };

            return response;
        }
    }
}
