using multiplixe.comum.dto;
using multiplixe.twitter.client;
using System;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.integracao_grpc
{
    public class TwitterOAuthObterURL : IIntegracaoGRPC<string>
    {
        private readonly Guid empresaId;
        private readonly string contaRedeSocial;

        public TwitterOAuthObterURL(Guid empresaId, string contaRedeSocial)
        {
            this.empresaId = empresaId;
            this.contaRedeSocial = contaRedeSocial;
        }

        public adduohelper.ResponseEnvelope<string> Enviar()
        {
            var client = new TwitterOAuthClient();
            return client.ObterURL(empresaId, contaRedeSocial);
        }
    }
}
