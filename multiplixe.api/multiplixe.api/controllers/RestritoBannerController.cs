using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using System.Collections.Generic;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/banner")]
    public class RestritoBannerController : AppRestritoController
    {
        public RestritoBannerController(
        IConfiguration configuration,
        EmpresaSettings empresaSettings) : base(configuration, empresaSettings)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarioId = ObterUsuarioId();

            var obterBanner = new integracao_grpc.ObterBanner(usuarioId);

            return IntegrarGRPC<List<comum_dto.Banner>>(obterBanner);
        }
    }
}