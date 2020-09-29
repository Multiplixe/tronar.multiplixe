using multiplixe.comum.dto.classificacao;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Dashboard
    {
        [JsonPropertyName("score")]
        public Classificacao Classificacao { get; set; }

        [JsonPropertyName("hasConnection")]
        public bool TemConexaoRedeSocial { get; set; }


        public Dashboard()
        {
        }
    }
}
