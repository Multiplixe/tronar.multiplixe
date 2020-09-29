using adduo.helper.envelopes;
using multiplixe.comum.enums;
using multiplixe.usuarios.grpc.protos;
using Google.Protobuf;
using System;
using System.Net;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.grpc.parsers
{
    public class TokenObter
    {
        public dto.Token Request(TokenFiltro filtro)
        {
            return new dto.Token
            {
                Tipo = (TipoTokenEnum)filtro.Tipo,
                UsuarioId = Guid.Parse(filtro.UsuarioId)
            };
        }

        public TokenResponse Response(ResponseEnvelope<dto.Token> envelope)
        {
            var response = new TokenResponse();

            response.HttpStatusCode = (int)envelope.HttpStatusCode;

            if (envelope.HttpStatusCode == HttpStatusCode.OK)
            {
                response.Valor = envelope.Item.Valor;
            }

            return response;

        }
    }
}
