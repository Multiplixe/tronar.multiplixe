using multiplixe.api.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace multiplixe.api.action_filters
{
    public class TwitchTravaPingDuploActionFilter : IAsyncActionFilter
    {
        private static Dictionary<string, string> trava { get; set; } = new Dictionary<string, string>();

        public TwitchTravaPingDuploActionFilter()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identity = context.HttpContext.User.Identity;

            if (Validar(identity, context))
            {
                await next();
            }

            context.Result = new BadRequestResult();

        }

        private bool Validar(IIdentity identity, ActionExecutingContext context)
        {
            var chaveHeader = "ping-key";

            var claimHelper = new ClaimHelper(identity);

            var user_id = claimHelper.ObterValor("user_id");

            var pingKey = context.HttpContext.Request.Headers[chaveHeader];

            var valido = false;

            if (!trava.ContainsKey(user_id) || trava[user_id] != pingKey)
            {
                valido = true;
                trava[user_id] = pingKey;
            }

            return valido;
        }
    }
}
