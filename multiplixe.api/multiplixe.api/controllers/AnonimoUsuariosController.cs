using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using adduohelper = adduo.helper;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("usuarios")]
    public class AnonimoUsuariosController : BaseController
    {
        public AnonimoUsuariosController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] adduohelper.envelopes.RequestEnvelope<comum_dto.entries.UsuarioRegistro> request)
        {
            ConfiguraEmpresa(request.Item);

            var registrarUsuario = new integracao_grpc.RegistrarUsuario(request);

            return IntegrarGRPC<comum_dto.entries.UsuarioRegistro>(registrarUsuario);
        }
    }
}
