using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.comum.exceptions;
using multiplixe.twitter.client.parsers;
using multiplixe.twitter.grpc.Protos;
using System;
using System.Data;

namespace multiplixe.twitter.client
{
    public class TwitterOAuthClient : BaseClient
    {
        private OAuth.OAuthClient client { get; set; }

        public TwitterOAuthClient()
        {
            client = new OAuth.OAuthClient(channel);
        }

        public ResponseEnvelope RegistrarPerfil(TwitterOAuthResponse twitterOAth)
        {
            var parser = new RegistrarPerfil();

            var request = parser.Request(twitterOAth);

            var response = client.RegistrarPerfil(request);

            var envelope = parser.Response(response);

            if (!envelope.Success)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

        public ResponseEnvelope<string> ObterURL(Guid empresaId, string contaRedeSocial)
        {
            var parser = new ObterURL();

            var request = parser.Request(empresaId, contaRedeSocial);

            var response = client.ObterURL(request);

            var envelope = parser.Response(response);

            if (!envelope.Success)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }
    }
}
