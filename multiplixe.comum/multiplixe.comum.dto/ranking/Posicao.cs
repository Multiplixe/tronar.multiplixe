using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.ranking
{
    public class Posicao
    {
        [JsonPropertyName("user")]
        public Guid UsuarioId { get; set; }
        
        [JsonPropertyName("name")]
        public string Nome { get; set; }
        
        [JsonPropertyName("nickname")]
        public string Apelido { get; set; }
        
        [JsonPropertyName("points")]
        public int Pontos { get; set; }
        
        [JsonPropertyName("value")]
        public int Valor { get; set; }

        [JsonPropertyName("currentUser")]
        public bool UsuarioAtual { get; set; }
    }
}
