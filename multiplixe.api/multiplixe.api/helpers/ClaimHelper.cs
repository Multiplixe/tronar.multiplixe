using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace multiplixe.api.helpers
{
    public class ClaimHelper
    {
        private ClaimsIdentity claimsIdentity { get; }

        public ClaimHelper(IIdentity identity)
        {
            claimsIdentity = (ClaimsIdentity)identity;

        }

        public string ObterValor(string chave)
        {
            var valor = string.Empty;

            if (claimsIdentity.Claims.Any(c => c.Type == chave))
            {
                var claim = claimsIdentity.Claims.First(a => a.Type == chave);
                valor = claim.Value;
            }

            return valor;
        }
    }
}
