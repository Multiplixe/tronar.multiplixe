using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.comum.dto.externo;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [Route("external/transaction")]
    [ApiController]
    public class ExternoTransacaoController : ExternoRestritoController
    {
        public ExternoTransacaoController(
            IConfiguration configuration,
            EmpresaSettings empresaSettings) : base(configuration, empresaSettings)
        {
        }

        [HttpPost]
        [Route("debit")]
        public IActionResult Post([FromBody] DebitoRequest request)
        {
            ConfiguraUsuario(request);
            ConfiguraEmpresa(request);

            var grpc = new integracao_grpc.TransacaoDebitar(request);

            return IntegrarGRPC<comum_dto.externo.DebitoResponse>(grpc);
        }
    }
}