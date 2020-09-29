using adduo.helper.envelopes;
using multiplixe.api.dto.settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using comum_dto =  multiplixe.comum.dto;
using multiplixe.api.consultas;

namespace multiplixe.api.controllers
{
    [Route("restrito/setup")]
    [ApiController]
    public class RestritoSetupController : AppRestritoController
    {
        private Setup setupService { get; }

        public RestritoSetupController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                Setup setupService
            ) : base(configuration, empresaSettings)

        {
            this.setupService = setupService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarioId = ObterUsuarioId();

                var setup = setupService.Get(usuarioId);

                var response = new ResponseEnvelope<comum_dto.Setup>(setup);

                return Ok(response);
            }
            catch (Exception ex)
            {
                //## TODO log

                return StatusCode(500, ex.Message);
            }
        }
    }
}