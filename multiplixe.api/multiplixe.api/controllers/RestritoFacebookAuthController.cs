using adduo.helper.envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using System;
using System.Net;
using System.Threading.Tasks;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.controllers
{

    [Route("restrito/facebook/oauth")]
    [ApiController]
    public class RestritoFacebookAuthController : AppRestritoController
    {

        public RestritoFacebookAuthController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
        }

        [HttpGet]
        [Route("authorize")]
        public IActionResult Autorizacao()
        {
            var empresaId = ObterEmpresaId();

            var integracao = new integracao_grpc.FacebookObterURLAutorizacao(empresaId);

            return IntegrarGRPC(integracao);
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Processar([FromBody] RequestEnvelope<facebook.dto.oauth.AuthResponse> envelope)
        {
            ConfiguraEmpresa(envelope.Item);
            ConfiguraUsuario(envelope.Item);

            var integracao = new integracao_grpc.FacebookProcessarCode(envelope.Item);

            return IntegrarGRPC(integracao);
        }



    }
}