using multiplixe.comum.dto.classificacao;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Setup
    {
        [JsonPropertyName("userProfile")]
        public Usuario usuario { get; set; }

        [JsonPropertyName("dashboard")]
        public Dashboard Dashboard { get; set; }

        [JsonPropertyName("ranking")]
        public ranking.Ranking Ranking { get; set; }
    }
}
