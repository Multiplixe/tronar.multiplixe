using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.comum.dto;
using multiplixe.twitch.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace multiplixe.twitch.client.parsers
{
    public class RegistrarPerfil
    {
        public RegistroRequest Request(TwitchOAuthResponse twitchOAth)
        {
            return new RegistroRequest
            {
                Code = twitchOAth.Code,
                UsuarioId = twitchOAth.UsuarioId.ToStringEmptyIfEmpty(),
                EmpresaId = twitchOAth.EmpresaId.ToStringEmptyIfEmpty(),
                ContaRedeSocial = twitchOAth.ContaRedeSocial
            };
        }

        public ResponseEnvelope Response(RegistroResponse response)
        {
            var envelope = new ResponseEnvelope();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (!envelope.Success)
            {
                envelope.Error.Messages.Add(response.Erro);
            }

            return envelope;
        }
    }
}
