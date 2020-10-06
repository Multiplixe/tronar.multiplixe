using adduo.helper.envelopes;
using multiplixe.classificador.grpc.Protos;
using System.Linq;

namespace multiplixe.classificador.grpc.parsers
{
    public class TransacaoEstornar
    {
        public EstornoResponse Response(ResponseEnvelope<comum.dto.externo.EstornoResponse> envelope)
        {
            var response = new EstornoResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode,
                TransacaoId = string.Empty
            };

            if(envelope.Success)
            {
                response.TransacaoId = envelope.Item.Id.ToString();
            }
            else if(envelope.Error.Messages.Any())
            {
                response.Erro = envelope.Error.Messages.First();
            }

            return response;
        }
    }
}
