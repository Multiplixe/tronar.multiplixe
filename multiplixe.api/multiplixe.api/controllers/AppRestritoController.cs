using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;

namespace multiplixe.api.controllers
{
    [Authorize(Policy = "app")]
    public class AppRestritoController : BaseRestritoController
    {
        public AppRestritoController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
        }
    }
}
