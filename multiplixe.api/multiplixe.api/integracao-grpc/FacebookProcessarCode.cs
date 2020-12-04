using adduo.helper.envelopes;
using multiplixe.facebook.client;
using multiplixe.facebook.dto.oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace multiplixe.api.integracao_grpc
{
    public class FacebookProcessarCode : IIntegracaoGRPC
    {
        private readonly AuthResponse request;

        public FacebookProcessarCode(facebook.dto.oauth.AuthResponse request)
        {
            this.request = request;
        }

        public ResponseEnvelope Enviar()
        {
            var client = new FacebookAutenticacaoClient();

            return client.ProcessarCode(request.Code, request.UsuarioId, request.EmpresaId);
        }
    }
}
