using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class Nivel
    {
        [JsonPropertyName("last")]
        public NivelItem Anterior { get; set; }
        
        [JsonPropertyName("current")]
        public NivelItemAtual Atual { get; set; }
        
        [JsonPropertyName("next")]
        public NivelItemProximo Proximo { get; set; }

        [JsonPropertyName("levelChanged")]
        public bool Mudou { get; set; }
    }
}
