using adduo.helper.extensionmethods;
using System.Net;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using proto = multiplixe.usuarios.grpc.protos;

namespace multiplixe.usuarios.client.parsers
{
    public class UsuarioExternoAutenticar
    {
        public proto.AutenticarRequest Request(dto.externo.AutenticacaoRequest autenticacaoRequest)
        {
            return new proto.AutenticarRequest
            {
                EmpresaId = autenticacaoRequest.EmpresaId.ToString(),
                Email = autenticacaoRequest.Email ?? string.Empty,
                Senha = autenticacaoRequest.Senha ?? string.Empty,
                ParceiroId = autenticacaoRequest.ParceiroId ?? string.Empty
            };
        }

        public adduohelper.ResponseEnvelope<dto.externo.AutenticacaoResponse> Response(proto.AutenticarResponse message)
        {
            var response = new adduohelper.ResponseEnvelope<dto.externo.AutenticacaoResponse>
            {
                HttpStatusCode = (HttpStatusCode)message.HttpStatusCode
            };

            if (response.Success)
            {
                response.Item.Token = message.Token;
                response.Item.Nome = message.Nome;
                response.Item.Apelido = message.Apelido;
            }
            else if (message.Erro.Mensagem.NotIsNullOrEmpty())
            {
                response.Error.Messages.Add(message.Erro.Mensagem);
            }

            return response;
        }
    }
}
