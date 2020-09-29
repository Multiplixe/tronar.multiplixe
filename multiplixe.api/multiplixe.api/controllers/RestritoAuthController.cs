using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/auth")]
    public class RestritoAuthController : AppRestritoController
    {

        public RestritoAuthController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)

        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
