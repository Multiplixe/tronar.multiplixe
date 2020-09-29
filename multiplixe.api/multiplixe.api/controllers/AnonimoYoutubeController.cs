using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.api.log_eventos;
using multiplixe.enfileirador.client;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("youtube")]
    public class AnonimoYoutubeController : BaseController
    {
        private YoutubeLogEventoService logEventoService { get; }

        public AnonimoYoutubeController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                YoutubeLogEventoService logEventoService,
                EnfileiradorClient enfileiradorClient) : base(configuration, empresaSettings)
        {
            this.enfileiradorClient = enfileiradorClient;
            this.logEventoService = logEventoService;
        }

        [HttpPost]
        public ActionResult Post()
        {
            var evento = new YoutubeEventoTest();
            logEventoService.LogarEvento(evento);
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            logEventoService.LogarRequestInicial(Request);
            return StatusCode(204, Request.Query["hub.challenge"]);
        }
    }
}