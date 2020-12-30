using adduo.helper.envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.comum.dto;
using System;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/twitter/oauth")]
    public class RestritoTwitterController : AppRestritoController
    {
        public RestritoTwitterController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings ) : base(configuration, empresaSettings)
        {
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Processar([FromBody] RequestEnvelope<TwitterOAuthResponse> request)
        {
            try
            {
                ConfiguraEmpresa(request.Item);
                ConfiguraUsuario(request.Item);

                var twitterRegistrarPerfil = new integracao_grpc.TwitterRegistrarPerfil(request.Item);

                return IntegrarGRPC(twitterRegistrarPerfil);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [Route("authenticate/{contaRedeSocial}")]
        public IActionResult Authenticate(string contaRedeSocial)
        {
            var empresaId = ObterEmpresaId();

            var twitterOAuthObterURL = new integracao_grpc.TwitterOAuthObterURL(empresaId, contaRedeSocial);

            return IntegrarGRPC<string>(twitterOAuthObterURL);
        }
    }
}