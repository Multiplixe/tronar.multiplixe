using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.oauth2.dtos
{
    public class Response
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}
