using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using System;
using System.Net;
using coredto = multiplixe.comum.dto;

namespace multiplixe.classificador.client.parsers
{
    public class ObterPontuacaoTotal
    {
        public ClassificacaoRequest Request(Guid usuarioId)
        {
            return new ClassificacaoRequest { UsuarioId = usuarioId.ToString() };
        }

        public ResponseEnvelope<coredto.classificacao.Pontuacao> Response(PontuacaoResponse pontuacaoResponse)
        {
            var responseEnvelope = new ResponseEnvelope<coredto.classificacao.Pontuacao>
            {
                HttpStatusCode = (HttpStatusCode)pontuacaoResponse.HttpStatusCode
            };

            if (responseEnvelope.Success)
            {
                responseEnvelope.Item = new coredto.classificacao.Pontuacao
                {
                    Valor = pontuacaoResponse.Pontuacao.Valor
                };
            }

            return responseEnvelope;
        }
    }
}
