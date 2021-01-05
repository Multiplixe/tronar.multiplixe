using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class TwitchPingResponse
    {
        [JsonPropertyName("pingTimeout")]
        public int FrequenciaMinutos { get; set; }

        [JsonPropertyName("title")]
        public string Chamada { get; set; }

        [JsonPropertyName("giveBackHeader")]
        public Dictionary<string, string> DevolverHeader { get; }

        [JsonPropertyName("score")]
        public classificacao.Classificacao Classificacao { get; set; }

        public TwitchPingResponse()
        {
            DevolverHeader = new Dictionary<string, string>();
        }

        public void AdicionarUltimoPing(string dataHoraEncript)
        {
            DevolverHeader.Add("ping-key", dataHoraEncript);
        }
    }
}
