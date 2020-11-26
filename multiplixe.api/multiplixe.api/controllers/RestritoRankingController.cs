using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.consultas;
using multiplixe.api.dto.settings;
using System;

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

            if (usuarioId == Guid.Parse("a694fdc0-7204-44a3-9e6a-47e56584d5d9"))
            {
                foreach (var item in response.Item.Posicoes)
                {
                    item.Nome += " " + DateTime.Now.Millisecond.ToString();
                    item.Apelido += " " + DateTime.Now.Millisecond.ToString();
                }
            }

            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}