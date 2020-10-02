using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class Saldo
    {
        [JsonPropertyName("value")]
        public int Valor { get; set; }
    }
}
