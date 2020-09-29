using adduohelper = adduo.helper;
using proto = multiplixe.usuarios.grpc.protos;
using dto = multiplixe.comum.dto;
using System.Net;
using System;
using multiplixe.comum.enums;

namespace multiplixe.usuarios.client.parsers
{
    class TokenObter
    {
        public proto.TokenFiltro Request(Guid usuarioId, TipoTokenEnum tipo)
        {
            return new proto.TokenFiltro
            {
                UsuarioId = usuarioId.ToString(),
                Tipo = (int)tipo
            };
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.Token> Response(proto.TokenResponse response)
        {
            var envelope = new adduohelper.envelopes.ResponseEnvelope<dto.Token>
            {
                HttpStatusCode = (HttpStatusCode)response.HttpStatusCode
            };

            if (envelope.Success)
            {
                envelope.Item = new dto.Token()
                {
                    Valor = response.Valor
                };
            }

            return envelope;
        }
    }
}
