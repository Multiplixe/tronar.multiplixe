using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.log_eventos;
using multiplixe.enfileirador.client;
using System;
using corehelper = multiplixe.comum.helper;
using facebook_dto = multiplixe.facebook.dto;
using comum_dto = multiplixe.comum.dto;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("facebook")]
    public class AnonimoFacebookController : BaseController
    {
        private FacebookLogEventoService logEventoService { get; }

        public AnonimoFacebookController(
            IConfiguration configuration,
            EmpresaSettings empresaSettings,
            FacebookLogEventoService logEventoService,
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
        public IActionResult Post([FromBody] object evento)
        {
            try
            {

                using (var sw = System.IO.File.AppendText("c:/log/face.txt"))
                {
                    sw.WriteLine("-----------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine("");
                    sw.WriteLine(evento);
                    sw.WriteLine("");

                    var json = corehelper.SerializadorHelper.Serializar(evento);

                    sw.WriteLine(json);
                }

               // logEventoService.LogarEvento(evento);

                //var envelope = new comum_dto.EnvelopeEvento<facebook_dto.eventos.Evento>(evento);
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
