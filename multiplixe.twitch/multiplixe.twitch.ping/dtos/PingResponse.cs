using System.Collections.Generic;
using System.Text.Json.Serialization;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.twitch.ping.dtos
{
    public class PingResponse
    {
        [JsonPropertyName("pingTimeout")]
        public int FrequenciaMinutos { get; set; }

        [JsonPropertyName("pingUrl")]
        public string PingUrl { get; set; }

        [JsonPropertyName("title")]
        public string Chamada { get; set; }

        [JsonPropertyName("giveBackHeader")]
        public Dictionary<string, string> DevolverHeader { get; }

        [JsonPropertyName("score")]
        public comum_dto.classificacao.Classificacao Classificacao { get; set; }

        public PingResponse()
        {
            DevolverHeader = new Dictionary<string, string>();
        }

        public void AdicionarUltimoPing(string dataHoraEncript)
        {
            DevolverHeader.Add("ping-key", dataHoraEncript);
        }
    }
}
