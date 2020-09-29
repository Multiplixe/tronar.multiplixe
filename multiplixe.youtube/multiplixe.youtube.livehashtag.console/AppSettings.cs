using System;

namespace  multiplixe.youtube.livehashtag.console
{
    public class AppSettings
    {
        public Guid empresa_id { get; set; }
        public string client_id { get; set; }
        public string apiKey { get; set; }
        public string refresh_token { get; set; }
        public string client_secret { get; set; }
        public AppSettingsUrl url { get; set; }

        public int segundos_entre_consultas { get; set; }
    }

    public class AppSettingsUrl
    {
        public string oauth2 { get; set; }
        public string liveBroadcasts { get; set; }
        public string messages { get; set; }

    }

}
