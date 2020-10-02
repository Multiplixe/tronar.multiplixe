using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.externo
{
    public class AutenticacaoResponse
    {
        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("nickname")]
        public string Apelido { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
