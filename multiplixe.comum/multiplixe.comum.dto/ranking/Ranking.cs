using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.ranking
{
    public class Ranking
    {
        [JsonPropertyName("date")]
        public DateTime DataProcessamento { get; set; }
        
        [JsonPropertyName("placing")]
        public List<Posicao> Posicoes { get; set; }

        public Ranking()
        {
            Posicoes = new List<Posicao>();
        }
    }
}
