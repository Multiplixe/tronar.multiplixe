using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using comum_dto = multiplixe.comum.dto;
using System.Net;
using multiplixe.comum.dto.externo;
using multiplixe.comum.enums;
using System.Linq;
using adduo.helper.extensionmethods;
using System;

namespace multiplixe.classificador.client.parsers
{
    public class TransacaoDebitar
    {
        public virtual DebitarRequest Request(comum_dto.externo.DebitoRequest request)
        {
            var debitarRequest = new DebitarRequest()
            {
                UsuarioId = request.UsuarioId.ToString(),
                EmpresaId = request.EmpresaId.ToString(),
                Descricao = request.Descricao ?? string.Empty,
                ParceiroId = request.ParceiroId ?? string.Empty,
                ParceiroTransacaoId = request.ParceiroTransacaoId ?? string.Empty,
                Pontos = request.Pontos
            };

            return debitarRequest;
        }

        public ResponseEnvelope<comum_dto.externo.DebitoResponse> Response(DebitarResponse debitarResponse)
        {
            var response = new ResponseEnvelope<comum_dto.externo.DebitoResponse>()
            {
                HttpStatusCode = (HttpStatusCode)debitarResponse.HttpStatusCode
            };

            if (response.Success)
            {
                var id = Guid.Empty;

                Guid.TryParse(debitarResponse.TransacaoId, out id);

                response.Item = new DebitoResponse
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
