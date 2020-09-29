using multiplixe.enfileirador.client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using multiplixe.api.log_eventos;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("instagram")]
    public class AnonimoInstagramController : BaseController
    {
        private InstagramLogEventoService logEventoService { get; }

        public AnonimoInstagramController(
            IConfiguration configuration,
            EmpresaSettings empresaSettings,
            InstagramLogEventoService logEventoService,
            EnfileiradorClient enfileiradorClient
            ) : base(configuration, empresaSettings)
        {
            this.enfileiradorClient = enfileiradorClient;
            this.logEventoService = logEventoService;
        }


        [HttpGet]
        public string Get()
        {
            logEventoService.LogarRequestInicial(Request);

            return Request.Query["hub.challenge"];
        }

        [HttpPost]
        public IActionResult Post([FromBody] InstagramEventTest evento)
        {
            try
            {
                Console.WriteLine("Proxy Facebook {0}", DateTime.Now.ToString());

                logEventoService.LogarEvento(evento);

                //var envelope = new dto.EnvelopeEvento<facebook_dto.eventos.Evento>(evento);
                //envelope.DataEvento = corehelper.DateTimeHelper.Now();

                //ConfiguraEmpresa(envelope);

                //enfileiradorClient.EnfileirarParaTriadorFacebook(envelope);

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
