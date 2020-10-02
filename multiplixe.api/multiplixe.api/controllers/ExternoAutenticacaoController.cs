using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [Route("external/auth")]
    [ApiController]
    public class ExternoAutenticacaoController : BaseController
    {
        public ExternoAutenticacaoController(IConfiguration configuration, EmpresaSettings empresaSettings) : base(configuration, empresaSettings)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] comum_dto.externo.AutenticacaoRequest request)
        {
            ConfiguraEmpresa(request);

            var autenticacao = new integracao_grpc.AutenticacaoExterna(request);

            return IntegrarGRPC<comum_dto.externo.AutenticacaoResponse>(autenticacao);
        }

    }
}