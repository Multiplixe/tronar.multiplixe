using multiplixe.comum.helper;
using multiplixe.api.dto.settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using adduohelper = adduo.helper.envelopes;
using comum_dto =  multiplixe.comum.dto;
using oauth = multiplixe.twitter.oauth;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/twitter")]
    public class RestritoTwitterController : AppRestritoController
    {
        private oauth.Servico oauthService { get; }

        public RestritoTwitterController(
                oauth.Servico oauthService,
                IConfiguration configuration,
                EmpresaSettings empresaSettings ) : base(configuration, empresaSettings)
        {
            this.oauthService = oauthService;
            this.enfileiradorClient = enfileiradorClient;
        }

        [HttpGet]
        [Route("oauth/process/{token}/{verifier}")]
        public async Task<IActionResult> Processar(string token, string verifier)
        {
            try
            {
                var perfil = await oauthService.Processar(token, verifier);

                var request = new adduohelper.RequestEnvelope<comum_dto.Perfil>(perfil);

                ConfiguraEmpresa(request.Item);
                ConfiguraUsuario(request.Item);

                var registrarPerfilIntegracaoGRPC = new integracao_grpc.RegistrarPerfil(request);

                return IntegrarGRPC<comum_dto.Perfil>(registrarPerfilIntegracaoGRPC);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

        [HttpGet]
        [Route("oauth/authenticate")]
        public async Task<IActionResult> Authenticate()
        {
            try
            {
                var envelope = await oauthService.Authenticate();

                return Ok(envelope);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private static StringContent StringContent(object dado)
        {
            var json = SerializadorHelper.Serializar(dado);
            return HttpHelper.StringContent(json);
        }
    }
}