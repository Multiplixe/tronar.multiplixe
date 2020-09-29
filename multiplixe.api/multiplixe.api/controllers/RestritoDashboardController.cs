using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.consultas;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/dashboard")]
    public class RestritoDashboardController : AppRestritoController
    {
        private Dashboard dashboard { get; }

        public RestritoDashboardController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                Dashboard dashboard
            ) : base(configuration, empresaSettings)
        {
            this.dashboard = dashboard;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var usuarioId = ObterUsuarioId();

            var response = this.dashboard.Obter(usuarioId);

            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
