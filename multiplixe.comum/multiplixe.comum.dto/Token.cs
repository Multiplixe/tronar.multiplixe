using multiplixe.comum.dto.interfaces;
using multiplixe.comum.enums;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Token : IUsuarioID
    {
        [JsonPropertyName("value")]
        public string Valor { get; set; }
        
        [JsonIgnore()]
        public Guid UsuarioId { get; set; }
        
        [JsonIgnore()]
        public TipoTokenEnum Tipo { get; set; }

        [JsonIgnore()]
        public DateTime? Expiracao { get; set; }
    }
}
