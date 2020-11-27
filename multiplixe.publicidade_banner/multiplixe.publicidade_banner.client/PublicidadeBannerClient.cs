using adduo.helper.envelopes;
using Grpc.Net.Client;
using multiplixe.comum.enums;
using multiplixe.comum.helper.grpc;
using multiplixe.publicidade_banner.grpc.Protos;
using System;
using System.Collections.Generic;
using coreexceptions = multiplixe.comum.exceptions;
using dto = multiplixe.comum.dto;

namespace multiplixe.publicidade_banner.client
{
    public class PublicidadeBannerClient
    {
        private GrpcChannel channel { get; set; }
        private Banner.BannerClient client { get; set; }

        public PublicidadeBannerClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.publicidadeBanner);
            client = new Banner.BannerClient(channel);
        }

        public ResponseEnvelope<List<dto.Banner>> ObterParaApp(Guid usuarioId)
        {
            var parser = new parsers.Obter();

            var request = parser.Request(usuarioId);

            var response = client.ObterParaApp(request);

            var envelope = parser.Response(response);

            if (!envelope.Success)
            {
                throw new coreexceptions.GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

    }
}
