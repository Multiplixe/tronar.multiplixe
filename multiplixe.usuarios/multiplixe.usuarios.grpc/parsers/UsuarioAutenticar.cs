using adduo.helper.extensionmethods;
using System.Linq;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;
namespace multiplixe.usuarios.grpc.parsers
{
    public class UsuarioAutenticar
    {

        public dto.externo.AutenticacaoRequest Request(proto.AutenticarRequest request)
        {
            return new dto.externo.AutenticacaoRequest
            {
                EmpresaId = request.EmpresaId.ToGuid(),
                Email = request.Email,
                Senha = request.Senha,
                ExternoId = request.ExternoId
            };
        }


        public proto.AutenticarResponse Response(adduohelper.ResponseEnvelope<dto.externo.AutenticacaoResponse> envelope)
        {
            var response = new proto.AutenticarResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode
            };

            if (envelope.Success)
            {
                response.Token = envelope.Item.Token;
                response.Nome = envelope.Item.Nome;
                response.Apelido = envelope.Item.Apelido;
            }
            else if(envelope.Error.Messages.Any())
            {
                response.Erro = new proto.ErroResponse
                {
                    Mensagem = envelope.Error.Messages.First()
                };
            }

            return response;
        }
    }
}
