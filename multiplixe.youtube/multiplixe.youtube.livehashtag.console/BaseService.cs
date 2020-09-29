using System.Collections.Generic;

namespace  multiplixe.youtube.livehashtag.console
{
    public class BaseService
    {
        protected AppSettings settings { get; }
        protected oauth2.Servico oauth2Servico { get; }

        public BaseService(AppSettings settings, oauth2.Servico oauth2Servico)
        {
            this.settings = settings;
            this.oauth2Servico = oauth2Servico;
        }

        protected Dictionary<string, string> ObterHeader()
        {
            return new Dictionary<string, string>
                {
                    {  "Authorization", $"Bearer {oauth2Servico.ObterToken()}" }
                };
        }
    }
}
