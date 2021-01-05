using adduo.helper.envelopes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [Route("restrito/compartilhamento")]
    [ApiController]
    public class RestritoCompartilhamentoController : AppRestritoController
    {
        public RestritoCompartilhamentoController(
        IConfiguration configuration,
        EmpresaSettings empresaSettings) : base(configuration, empresaSettings)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            //var empresaId = ObterEmpresaId();
            //var compartilhamentoObter = new integracao_grpc.CompartilhamentoObter(empresaId);

            return null; // IntegrarGRPC<List<comum_dto.Compartilhamento>>(compartilhamentoObter);
        }

        [HttpPost]
        public IActionResult PostCompartilhar([FromBody] RequestEnvelope<comum_dto.Compartilhamento> request)
        {
            ConfiguraEmpresa(request.Item);
            //ConfiguraUsuario(request.Item);

            var grpc = new integracao_grpc.Compartilhamento(request);

            return IntegrarGRPC(grpc);
        }
    }
}