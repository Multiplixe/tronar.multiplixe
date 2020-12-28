using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class NivelItemProximo : NivelItem
    {
        [JsonPropertyName("points")]
        public int Pontos { get; set; }
    }
}
