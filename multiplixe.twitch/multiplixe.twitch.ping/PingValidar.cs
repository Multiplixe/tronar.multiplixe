using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.twitch.ping
{
    public class PingValidar
    {
        private static Dictionary<string, string> trava { get; set; } = new Dictionary<string, string>();

        public void Validar(string is_unlinked, string twitchUserId, string pingKeyHeader)
        {
            var valido = is_unlinked == "false" && !string.IsNullOrEmpty(twitchUserId);

            if (valido &&
                !trava.ContainsKey(twitchUserId) ||
                trava[twitchUserId] != pingKeyHeader)
            {
                valido = true;
                trava[twitchUserId] = pingKeyHeader;
            }

            if(!valido)
            {
                throw new ArgumentException();
            }
        }

    }
}
