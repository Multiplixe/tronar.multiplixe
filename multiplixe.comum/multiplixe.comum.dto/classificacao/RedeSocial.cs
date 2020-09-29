using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class RedeSocial : ItemGenerico
    {
        [JsonPropertyName("points")]
        public int Pontos { get; set; }

        [JsonPropertyName("percent")]
        public int Percent { get; set; }

        [JsonPropertyName("connected")]
        public bool Conectado { get; set; }

        [JsonPropertyName("profile")]
        public PerfilBasico Perfil { get; set; }

        public RedeSocial()
        {
        }
    }
}
