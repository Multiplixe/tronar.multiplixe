using adduo.helper.envelopes;
using multiplixe.comum.exceptions;
using multiplixe.twitter.client.parsers;
using multiplixe.twitter.grpc.Protos;
using System;
using System.Data;

namespace multiplixe.twitter.client
{
    public class TwitterWebhookClient : BaseClient
    {
        private Webhook.WebhookClient client { get; set; }

        public TwitterWebhookClient()
        {
            client = new Webhook.WebhookClient(channel);
        }

        public ResponseEnvelope<comum.dto.TwitterCRCResponse> ProcessarCRC(string crc, Guid empresaId, string contaRedeSocial)
        {
            var parser = new ProcessarCRC();

            var request = parser.Request(crc, empresaId, contaRedeSocial);

            var response = client.ProcessarCRC(request);

            var envelope = parser.Response(response);

            if (!envelope.Success)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }
    }
}
