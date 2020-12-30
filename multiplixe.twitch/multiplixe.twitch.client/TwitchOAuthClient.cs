using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.comum.exceptions;
using multiplixe.twitch.client.parsers;
using multiplixe.twitch.grpc.Protos;
using System;

namespace multiplixe.twitch.client
{
    public class TwitchOAuthClient : BaseClient
        {
            private OAuth.OAuthClient client { get; set; }

            public TwitchOAuthClient()
            {
                client = new OAuth.OAuthClient(channel);
            }

            public ResponseEnvelope RegistrarPerfil(TwitchOAuthResponse twitchOAth)
            {
                var parser = new RegistrarPerfil();

                var request = parser.Request(twitchOAth);

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
