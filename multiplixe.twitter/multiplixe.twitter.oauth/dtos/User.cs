using System.Text.Json.Serialization;

namespace multiplixe.twitter.oauth.dtos
{
    public class User
    {
        [JsonPropertyName("id_str")]
        public string id { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string profile_image_url_https { get; set; }
    }
}
