using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [Route("external/query")]
    [ApiController]
    public class ExternoConsultasController : ExternoRestritoController
    {
        public ExternoConsultasController(
            IConfiguration configuration,
            EmpresaSettings empresaSettings) : base(configuration, empresaSettings)
        {
        }

        [HttpGet]
        [Route("check")]
        public IActionResult Get()
        {
            return Ok();
        }


    }
}