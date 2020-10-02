using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [Authorize(Policy = "external")]
    public class ExternoRestritoController : BaseRestritoController
    {
        public ExternoRestritoController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
        }
    }
}
