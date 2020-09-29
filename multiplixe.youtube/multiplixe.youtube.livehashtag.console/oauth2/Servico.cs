using multiplixe.comum.helper;
using  multiplixe.youtube.livehashtag.console.oauth2.dtos;

namespace  multiplixe.youtube.livehashtag.console.oauth2
{
    public class Servico
    {
        private AppSettings settings { get; }

        private string token { get; set; }

        public Servico(AppSettings settings)
        {
            this.settings = settings;

            Get();
        }

        public string ObterToken()
        {
            return token;
        }

        private void Get()
        {
            var oauth2UrlRequest = new
            {
                this.settings.client_id,
                this.settings.client_secret,
                grant_type = "refresh_token",
                this.settings.refresh_token,
            };

            var oauth2UrlResponse = WebRequestHelper.PostExterno<Response>(this.settings.url.oauth2, oauth2UrlRequest);
            oauth2UrlResponse.ThrownIfError();

            token = oauth2UrlResponse.Item.Token;
        }

    }
}
