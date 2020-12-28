using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class NivelItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("show")]
        public bool Mostrar { get; set; }
    }
}
