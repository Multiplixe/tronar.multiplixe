using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.usuarios.client;
using System;

namespace multiplixe.api.integracao_grpc
{
    public class UltimoAcesso : IIntegracaoGRPC
    {
        private readonly UsuarioUltimoAcesso usuarioUltimoAcesso;

        public UltimoAcesso(UsuarioUltimoAcesso usuarioUltimoAcesso)
        {
            this.usuarioUltimoAcesso = usuarioUltimoAcesso;
        }

        public ResponseEnvelope Enviar()
        {
            var client = new UsuarioClient();

            return client.UltimoAcesso(usuarioUltimoAcesso);
        }
    }
}
