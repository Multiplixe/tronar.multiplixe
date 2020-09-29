using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using System;
using System.Threading.Tasks;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;
using twitchoauth = multiplixe.twitch.oauth;

namespace multiplixe.api.controllers
{

    [Route("restrito/twitch/oauth")]
    [ApiController]
    public class RestritoTwitchAuthController : AppRestritoController
    {
        private twitchoauth.Servico oauthService { get; }
        public RestritoTwitchAuthController(
                twitchoauth.Servico oauthService,
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
            this.oauthService = oauthService;
        }

        [HttpGet]
        [Route("authorize")]
        public IActionResult Autorizacao()
        {
            var url = oauthService.Autorizar();
            var response = new adduohelper.ResponseEnvelope<string>(url);
            return Ok(response);
        }


        [HttpGet]
        [Route("process/{code}")]
        public async Task<IActionResult> Processar(string code)
        {
            try
            {
                var perfil = await oauthService.Processar(code);

                var request = new adduohelper.RequestEnvelope<comum_dto.Perfil>(perfil);

                ConfiguraEmpresa(request.Item);
                ConfiguraUsuario(request.Item);

                var registrarPerfilIntegracaoGRPC = new integracao_grpc.RegistrarPerfil(request);

                return IntegrarGRPC<comum_dto.Perfil>(registrarPerfilIntegracaoGRPC);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}