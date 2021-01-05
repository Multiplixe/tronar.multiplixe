using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using multiplixe.api.action_filters;
using multiplixe.api.helpers;
using multiplixe.api.dto.settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using twitch_dto = multiplixe.twitch.dto;
using multiplixe.twitch.ping;
using System;
using System.Diagnostics;
using System.Net;
using comum_dto = multiplixe.comum.dto;
using multiplixe.comum.dto;
using multiplixe.twitch.client;

namespace multiplixe.api.controllers
{

    [Route("restrito/twitch/ping")]
    [ApiController]
    [Authorize(Policy = "twitch")]
    public class RestritoTwitchController : BaseRestritoController
    {
        private readonly string pingKeyNameHeader = "ping-key";
        private readonly string pingPausaNameHeader = "ping-pause";
        private readonly string pingEmpresaNameHeader = "empresa-id";
        private readonly string channel_id = "channel_id";
        private readonly string user_id = "user_id";
        private readonly string is_unlinked = "is_unlinked";

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
        [ServiceFilter(typeof(TwitchValidacaoPingActionFilter))]
        public IActionResult PingInicial()
        {
            var twitchUserId = ObterValorDoClaims(user_id);

            var empresaId = ObterValorDoHeader(pingEmpresaNameHeader);

            var integracaoGrpc = new integracao_grpc.TwitchPingInicial(twitchUserId, Guid.Parse(empresaId));

            return IntegrarGRPC<TwitchPingResponse>(integracaoGrpc);
        }


        [HttpPost]
        public IActionResult Ping()
        {
            var twitchChannelId = ObterValorDoClaims(channel_id);
            var twitchUserId = ObterValorDoClaims(user_id);
            var twitchIsUnlinked = ObterValorDoClaims(is_unlinked);

            var pingPausa = ObterValorDoHeader(pingPausaNameHeader);
            var pingKey = ObterValorDoHeader(pingKeyNameHeader);

            var empresaId = ObterValorDoHeader(pingEmpresaNameHeader);

            var integracaoGrpc = new integracao_grpc.TwitchPing(twitchUserId, twitchChannelId, twitchIsUnlinked, pingKey, pingPausa, Guid.Parse(empresaId));

            return IntegrarGRPC<TwitchPingResponse>(integracaoGrpc);
        }
    }
}