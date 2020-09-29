using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.api.helpers;
using multiplixe.comum.dto.interfaces;
using System;

namespace multiplixe.api.controllers
{
    public class BaseRestritoController : BaseController
    {
        public BaseRestritoController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings
            ) : base(configuration, empresaSettings)
        {
        }

        public void ConfiguraUsuario(IUsuarioID dto)
        {
            dto.UsuarioId = ObterUsuarioId();
        }

        protected Guid ObterUsuarioId()
        {
            var usuarioId = Guid.Empty;

            var identity = User.Identity;

            var claimHelper = new ClaimHelper(identity);

            if (identity != null && identity.IsAuthenticated)
            {
                usuarioId = Guid.Parse(claimHelper.ObterValor("user_id"));
            }

            return usuarioId;
        }

    }
}
