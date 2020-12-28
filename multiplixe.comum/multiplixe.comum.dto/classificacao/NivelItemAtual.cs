using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class NivelItemAtual : NivelItem
    {
        [JsonPropertyName("reachNext")]
        public int PontosParaProximoNivel { get; set; }
    }
}
