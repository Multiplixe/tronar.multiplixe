using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.consultas;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/ranking")]
    public class RestritoRankingController : AppRestritoController
    {
        private Ranking ranking { get; }

        public RestritoRankingController(
                  IConfiguration configuration,
                  EmpresaSettings empresaSettings,
                  Ranking ranking
              ) : base(configuration, empresaSettings)
        {
            this.ranking = ranking;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var usuarioId = ObterUsuarioId();

            var response = ranking.Obter(usuarioId);

            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}