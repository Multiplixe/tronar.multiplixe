using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class PerfilBasico
    {
        [JsonPropertyName("profileId")]
        public string PerfilId { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("nickname")]
        public string Apelido { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImagemUrl { get; set; }
    }
}
