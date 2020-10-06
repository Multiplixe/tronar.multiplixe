using adduo.helper.envelopes;
using System;
using System.Net;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.classificador.client.parsers
{
    public class TransacaoEstorno
    {
        public virtual grpc.Protos.EstornoRequest Request(comum_dto.externo.EstornoRequest request)
        {
            var estornoRequest = new grpc.Protos.EstornoRequest()
            {
                ParceiroId = request.ParceiroId.ToString(),
                TransacaoId = request.TransacaoId.ToString(),
            };

            return estornoRequest;
        }

        public ResponseEnvelope<comum_dto.externo.EstornoResponse> Response(grpc.Protos.EstornoResponse estornoResponse)
        {
            var response = new ResponseEnvelope<comum_dto.externo.EstornoResponse>()
            {
                HttpStatusCode = (HttpStatusCode)estornoResponse.HttpStatusCode
            };

            if (response.Success)
            {
                var id = Guid.Empty;

                Guid.TryParse(estornoResponse.TransacaoId, out id);

                response.Item = new comum_dto.externo.EstornoResponse
                {
                    Id = id
                };
            }
            else if (!string.IsNullOrEmpty(estornoResponse.Erro))
            {
                response.Error.Messages.Add(estornoResponse.Erro);
            }

            return response;
        }
    }
}
