using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class Nivel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("levelChanged")]
        public bool Mudou { get; set; }
    }
}
