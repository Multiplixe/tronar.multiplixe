using multiplixe.twitch.client;
using System;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.integracao_grpc
{
    public class TwitchOAuthObterURL : IIntegracaoGRPC<string>
    {
        private readonly Guid empresaId;
        private readonly string contaRedeSocial;

        public TwitchOAuthObterURL(Guid empresaId, string contaRedeSocial)
        {
            this.empresaId = empresaId;
            this.contaRedeSocial = contaRedeSocial;
        }

        public adduohelper.ResponseEnvelope<string> Enviar()
        {
            var client = new TwitchOAuthClient();
            return client.ObterURL(empresaId, contaRedeSocial);
        }
    }
}
