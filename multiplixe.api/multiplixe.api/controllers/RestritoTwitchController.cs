using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using multiplixe.api.action_filters;
using multiplixe.api.helpers;
using multiplixe.api.dto.settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using twitch_dto =  multiplixe.twitch.dto;
using multiplixe.twitch.ping;
using System;
using System.Diagnostics;
using System.Net;
using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.controllers
{

    [Route("restrito/twitch")]
    [ApiController]
    [Authorize(Policy = "twitch")]
    public class RestritoTwitchController : BaseRestritoController
    {
        private PingService pingService { get; }

        public RestritoTwitchController(
                IConfiguration configuration,
                EnfileiradorClient enfileiradorClient,
                EmpresaSettings empresaSettings,
                PingService pingService
            ) : base(configuration, empresaSettings)
        {
            base.enfileiradorClient = enfileiradorClient;
            this.pingService = pingService;
        }


        [HttpGet]
        [Route("ping-inicial")]
        [ServiceFilter(typeof(TwitchValidacaoPingActionFilter))]
        public IActionResult PingInicial()
        {
            var identity = User.Identity;
            var claimHelper = new ClaimHelper(identity);
            var user_id = claimHelper.ObterValor("user_id");

            var response = pingService.PingInicial(user_id, empresaSettings.Id);

            Debug.WriteLine("Init {0}", DateTimeHelper.Now());

            return StatusCode((int)response.HttpStatusCode, response);
        }


        [HttpPost]
        [Route("ping")]
        [ServiceFilter(typeof(TwitchValidacaoPingActionFilter))]
        [ServiceFilter(typeof(TwitchTravaPingDuploActionFilter))]
        public IActionResult Ping()
        {
            var httpStatusCode = HttpStatusCode.OK;

            try
            {
                EnfileirarPing();
            }
            catch (Exception ex)
            {
                //## TODO
                httpStatusCode = HttpStatusCode.InternalServerError;
            }

            var response = pingService.ProximoPingkey();
            response.HttpStatusCode = httpStatusCode;

            return StatusCode((int)response.HttpStatusCode, response);
        }

        private void EnfileirarPing()
        {
            var identity = User.Identity;

            var claimHelper = new ClaimHelper(identity);

            var channelId = claimHelper.ObterValor("channel_id");
            var user_id = claimHelper.ObterValor("user_id");

            var evento = pingService.GerarEvento(Request.Headers, user_id, channelId);

            var envelope = new comum_dto.EnvelopeEvento<twitch_dto.eventos.Evento>(evento);
            envelope.DataEvento = evento.Ping.Atual;

            ConfiguraEmpresa(envelope);

            enfileiradorClient.EnfileirarParaTriadorTwitch(envelope);
        }

    }
}