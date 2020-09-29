namespace multiplixe.twitch.oauth.dtos
{
    public class AuthContext
    {
        public string ClientSecret { get; set; }
        public string ClientID { get; set; }
        public string ExtensionSecret { get; set; }
        public string UrlAuth { get; set; }
        public string UrlApi { get; set; }
        public string Scope { get; set; }
        public string RedirectUrl { get; set; }
    }
}
