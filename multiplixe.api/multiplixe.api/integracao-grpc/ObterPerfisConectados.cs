using adduohelper = adduo.helper.envelopes;
using comum_dto =  multiplixe.comum.dto;
using usuario = multiplixe.usuarios.client;
using System;

namespace multiplixe.api.integracao_grpc
{
    public class ObterPerfisConectados : IIntegracaoGRPC<comum_dto.RedesSociaisPerfisConectados>
    {
        private Guid usuarioId { get; }

        public ObterPerfisConectados(Guid usuarioId)
        {
            this.usuarioId = usuarioId;
        }

        public adduohelper.ResponseEnvelope<comum_dto.RedesSociaisPerfisConectados> Enviar()
        {
            var perfilClient = new usuario.PerfilClient();
            return perfilClient.ObterPerfisConectados(usuarioId);
        }
    }
}
