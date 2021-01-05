using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.Compartilhamento
{
    public class Compartilhamento
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("url")]
        public bool Url { get; set; }
    }
}