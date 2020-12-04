using adduo.helper.envelopes;
using multiplixe.facebook.client;
using System;

namespace multiplixe.api.integracao_grpc
{
    public class FacebookObterURLAutorizacao : IIntegracaoGRPC<string>
    {
        private readonly Guid empresaId;

        public FacebookObterURLAutorizacao(Guid empresaId)
        {
            this.empresaId = empresaId;
        }

        public ResponseEnvelope<string> Enviar()
        {
            var client = new FacebookAutenticacaoClient();

            return client.ObterURLAutorizacao(empresaId);
        }
    }

}
