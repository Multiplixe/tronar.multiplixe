using adduohelper = adduo.helper;
using proto = multiplixe.usuarios.grpc.protos;
using dto = multiplixe.comum.dto;
using System.Net;
using multiplixe.comum.enums;
using System;

namespace multiplixe.usuarios.client.parsers
{
    class TokenRegistrar
    {
        public proto.TokenRequest Request(Guid usuarioId, string valor, TipoTokenEnum tipo)
        {
            return new proto.TokenRequest
            {
                UsuarioId = usuarioId.ToString(),
                Tipo = (int)tipo,
                Valor = valor
            };
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.Token> Response(proto.TokenResponse response)
        {
            var envelope = new adduohelper.envelopes.ResponseEnvelope<dto.Token>();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            return envelope;
        }

    }
}
