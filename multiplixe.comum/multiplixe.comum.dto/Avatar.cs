using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Avatar
    {
        [JsonPropertyName("image")]
        public string Imagem { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
    }
}
