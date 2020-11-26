using Grpc.Core;
using multiplixe.publicidade_banner.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.publicidade_banner.grpc.Services
{
    public class BannerService : Banner.BannerBase
    {
        private readonly Servico servico;

        public BannerService(Servico servico)
        {
            this.servico = servico;
        }

        public override Task<ObterResponse> ObterParaApp(ObterRequest request, ServerCallContext context)
        {
            var response = new ObterResponse();

            try
            {
                var usuarioId = Guid.Parse(request.UsuarioId);

                var banners = servico.Obter(usuarioId);

                response.Banners.AddRange(banners);
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);
        }


    }
}
