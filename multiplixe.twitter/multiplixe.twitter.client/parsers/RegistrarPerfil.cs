using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.comum.dto;
using multiplixe.twitter.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.twitter.client.parsers
{
    public class RegistrarPerfil
    {
        public RegistroRequest Request(TwitterOAuthResponse twitterOAth)
        {
            return new RegistroRequest
            {
                Token = twitterOAth.Token,
                Verifier = twitterOAth.Verifier,
                UsuarioId = twitterOAth.UsuarioId.ToStringEmptyIfEmpty(),
                EmpresaId = twitterOAth.EmpresaId.ToStringEmptyIfEmpty(),
                ContaRedeSocial = twitterOAth.ContaRedeSocial
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
