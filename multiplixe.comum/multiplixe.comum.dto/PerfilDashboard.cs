using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class PerfilDashboard
    {
        [JsonPropertyName("profile")]
        public Perfil Perfil { get; set; }
    }
}
