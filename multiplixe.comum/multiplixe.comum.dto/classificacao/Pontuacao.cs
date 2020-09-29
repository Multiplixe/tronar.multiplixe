using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class Pontuacao
    {
        [JsonPropertyName("value")]
        public int Valor { get; set; }
    }
}
