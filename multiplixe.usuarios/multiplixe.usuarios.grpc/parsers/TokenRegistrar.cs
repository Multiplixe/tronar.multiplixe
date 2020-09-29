using adduohelper = adduo.helper.envelopes;
using multiplixe.usuarios.grpc.protos;
using System;
using dto = multiplixe.comum.dto;
using multiplixe.comum.enums;

namespace multiplixe.usuarios.grpc.parsers
{
    public class TokenRegistrar
    {
        public dto.Token Request(TokenRequest request)
        {
            var dto = new dto.Token
            {
                Valor = request.Valor,
                UsuarioId = Guid.Parse(request.UsuarioId),
                Tipo = (TipoTokenEnum)request.Tipo
            };

            return dto;
        }

        public TokenResponse Response(adduohelper.ResponseEnvelope envelope)
        {
            var response = new TokenResponse
            {
                HttpStatusCode = (int)envelope.HttpStatusCode
            };

            return response;
        }
    }
}
