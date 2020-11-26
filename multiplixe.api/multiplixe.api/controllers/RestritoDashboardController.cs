using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.consultas;
using multiplixe.api.dto.settings;
using System.Linq;

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

            if (usuarioId == System.Guid.Parse("a694fdc0-7204-44a3-9e6a-47e56584d5d9"))
            {
                var s = System.DateTime.Now.Second;

                foreach (var item in response.Item.Classificacao.RedesSociais)
                {
                    item.Pontos +=  s + item.Id;
                }

                var total = response.Item.Classificacao.RedesSociais.Sum(s => s.Pontos);

                response.Item.Classificacao.Pontuacao.Valor = total;
                response.Item.Classificacao.Saldo.Valor = s;

                foreach (var item in response.Item.Classificacao.RedesSociais)
                {
                    item.Percent = item.Pontos * 100 / total;
                }

            }


            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
