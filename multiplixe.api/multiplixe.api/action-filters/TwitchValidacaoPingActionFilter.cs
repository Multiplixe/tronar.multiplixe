using multiplixe.api.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ping = multiplixe.twitch.ping.dtos;
using System.Security.Principal;
using System.Threading.Tasks;

namespace multiplixe.api.action_filters
{
    public class TwitchValidacaoPingActionFilter : IAsyncActionFilter
    {
        private ping.PingConfig config { get; }

        public TwitchValidacaoPingActionFilter(ping.PingConfig config)
        {
            this.config = config;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identity = context.HttpContext.User.Identity;

            if (Validar(identity))
            {
                await next();
            }

            context.Result = new BadRequestResult();

        }

        private bool Validar(IIdentity identity)
        {
            var claimHelper = new ClaimHelper(identity);

            var is_unlinked = claimHelper.ObterValor("is_unlinked");
            var user_id = claimHelper.ObterValor("user_id");

            var valido = is_unlinked == "false" &&
                        !string.IsNullOrEmpty(user_id);

            return valido;
        }
    }
}
