using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.api.log_eventos;
using multiplixe.comum.exceptions;
using multiplixe.enfileirador.client;
using multiplixe.twitter.client;
using System;
using System.Security.Cryptography;
using System.Text;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;
using twitter_dto = multiplixe.twitter.dto;
using twitteroauth = multiplixe.twitter.oauth;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("twitter")]
    public class AnonimoTwitterController : BaseController
    {
        private TwitterLogEventoService twitterLogEventoService { get; }
        private twitteroauth.dtos.AuthContext authContext { get; }

        public AnonimoTwitterController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                TwitterLogEventoService twitterLogEventoService,
                EnfileiradorClient enfileiradorClient,
                twitteroauth.dtos.AuthContext authContext) : base(configuration, empresaSettings)
        {
            this.enfileiradorClient = enfileiradorClient;
            this.twitterLogEventoService = twitterLogEventoService;
            this.authContext = authContext;
        }

        [HttpPost]
        [Route("{empresaId}/{username}")]
        public ActionResult Post([FromBody] twitter_dto.eventos.Evento evento, string empresaId, string username)
        {
            twitterLogEventoService.LogarEvento(evento);

            var envelope = new comum_dto.EnvelopeEvento<twitter_dto.eventos.Evento>(evento);
            envelope.DataEvento = corehelper.DateTimeHelper.Now();

            ConfiguraEmpresa(envelope);

            enfileiradorClient.EnfileirarParaTriadorTwitter(envelope);

            return Ok();
        }

        [HttpGet]
        [Route("{empresaId}/{username}")]
        public ActionResult Get([FromQuery(Name = "crc_token")] string crc_token, Guid empresaId, string username)
        {
            twitterLogEventoService.LogarRequestInicial(Request);

            try
            {
                var client = new TwitterWebhookClient();
                var response = client.ProcessarCRC(crc_token, empresaId, username);

                return Ok(response.Item);
            }
            catch (GRPCException ex)
            {
                return StatusCode((int)ex.HttpStatusCode);
            }
        }
    }
}