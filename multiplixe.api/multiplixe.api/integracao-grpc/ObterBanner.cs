using multiplixe.publicidade_banner.client;
using System;
using System.Collections.Generic;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class ObterBanner : IIntegracaoGRPC<List<comum_dto.Banner>>
    {
        private Guid usuarioId { get; }

        public ObterBanner(Guid usuarioId)
        {
            this.usuarioId = usuarioId;
        }

        public adduohelper.ResponseEnvelope<List<comum_dto.Banner>> Enviar()
        {
            var client = new PublicidadeBannerClient();

            return client.ObterParaApp(usuarioId);
        }
    }
}
