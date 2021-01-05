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

        protected string ObterValorDoClaims(string chave)
        {
            var claimHelper = new ClaimHelper(User.Identity);

            return claimHelper.ObterValor(chave);
        }

        protected Guid ObterUsuarioId()
        {
            var usuarioId = Guid.Empty;

            var identity = User.Identity;

            var claimHelper = new ClaimHelper(identity);

            if (identity != null && identity.IsAuthenticated)
            {
                if (!Guid.TryParse(claimHelper.ObterValor("user_id"), out usuarioId))
                {
                    throw new ArgumentException("Usuário inválido");
                }
            }

            return usuarioId;
        }

        protected string ObterValorDoHeader(string key)
        {
            var valor = string.Empty;

            if (Request.Headers.ContainsKey(key))
            {
                valor = Request.Headers[key];
            }

            return valor;
        }

    }
}
