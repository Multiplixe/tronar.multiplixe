using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class Ranking
    {
        [JsonPropertyName("value")]
        public int Valor { get; set; }
    }
}
