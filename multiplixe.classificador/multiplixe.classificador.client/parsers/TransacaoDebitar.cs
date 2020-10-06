using adduo.helper.envelopes;
using System;
using System.Net;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.classificador.client.parsers
{
    public class TransacaoDebitar
    {
        public virtual grpc.Protos.DebitoRequest Request(comum_dto.externo.DebitoRequest request)
        {
            var debitarRequest = new grpc.Protos.DebitoRequest()
            {
                UsuarioId = request.UsuarioId.ToString(),
                EmpresaId = request.EmpresaId.ToString(),
                Descricao = request.Descricao ?? string.Empty,
                ParceiroId = request.ParceiroId.ToString(),
                ParceiroTransacaoId = request.ParceiroTransacaoId ?? string.Empty,
                Pontos = request.Pontos
            };

            return debitarRequest;
        }

        public ResponseEnvelope<comum_dto.externo.DebitoResponse> Response(grpc.Protos.DebitoResponse debitarResponse)
        {
            var response = new ResponseEnvelope<comum_dto.externo.DebitoResponse>()
            {
                HttpStatusCode = (HttpStatusCode)debitarResponse.HttpStatusCode
            };

            if (response.Success)
            {
                var id = Guid.Empty;

                Guid.TryParse(debitarResponse.TransacaoId, out id);

                response.Item = new comum_dto.externo.DebitoResponse
                {
                    Id = id
                };
            }
            else if (!string.IsNullOrEmpty(debitarResponse.Erro))
            {
                response.Error.Messages.Add(debitarResponse.Erro);
            }

            return response;
        }
    }
}
