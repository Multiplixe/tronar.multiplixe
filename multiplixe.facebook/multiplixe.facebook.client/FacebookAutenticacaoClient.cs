using adduo.helper.envelopes;
using multiplixe.facebook.client.parsers;
using multiplixe.facebook.grpc.Protos;
using System;

namespace multiplixe.facebook.client
{
    public class FacebookAutenticacaoClient : BaseClient
    {
        private Autenticacao.AutenticacaoClient client { get; set; }

        public FacebookAutenticacaoClient()
        {
            client = new Autenticacao.AutenticacaoClient(channel);
        }

        public ResponseEnvelope ProcessarCode(string code, Guid usuarioId, Guid empresaId)
        {
            var parser = new ProcessarCode();

            var request = parser.Request(code, usuarioId, empresaId);

            var response = client.ProcessarCode(request);

            var envelope = parser.Response(response);

            return envelope;
        }

        public ResponseEnvelope<string> ObterURLAutorizacao(Guid empresaId)
        {
            var parser = new ObterURLAutorizacao();

            var request = parser.Request(empresaId);

            var response = client.ObterURLAutorizacao(request);

            var envelope = parser.Response(response);

            return envelope;
        }
    }
}
