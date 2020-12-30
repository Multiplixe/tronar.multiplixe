using adduo.helper.envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.comum.dto;
using System;

namespace multiplixe.api.controllers
{

    [Route("restrito/twitch/oauth")]
    [ApiController]
    public class RestritoTwitchAuthController : AppRestritoController
    {
        public RestritoTwitchAuthController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
        }

        [HttpGet]
        [Route("authenticate/{contaRedeSocial}")]
        public IActionResult Authenticate(string contaRedeSocial)
        {
            var empresaId = ObterEmpresaId();

            var twitchOAuthObterURL = new integracao_grpc.TwitchOAuthObterURL(empresaId, contaRedeSocial);

            return IntegrarGRPC<string>(twitchOAuthObterURL);
        }


        [HttpPost]
        [Route("process")]
        public IActionResult Processar([FromBody] RequestEnvelope<TwitchOAuthResponse> request)
        {
            try
            {
                ConfiguraEmpresa(request.Item);
                ConfiguraUsuario(request.Item);

                var twitchRegistrarPerfil = new integracao_grpc.TwitchRegistrarPerfil(request.Item);

                return IntegrarGRPC(twitchRegistrarPerfil);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}