using adduo.helper.envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [Route("restrito/compartilhamento")]
    [ApiController]
    public class CompartilhamentoController : AppRestritoController
    {
        public CompartilhamentoController(
        IConfiguration configuration,
        EmpresaSettings empresaSettings) : base(configuration, empresaSettings)
        {
        }

        [HttpPost]
        public IActionResult PostCompartilhar([FromBody] RequestEnvelope<comum_dto.Compartilhamento> request)
        {
            ConfiguraEmpresa(request.Item);
            ConfiguraUsuario(request.Item);

            var grpc = new integracao_grpc.Compartilhamento(request);

            return IntegrarGRPC(grpc);
        }
    }
}