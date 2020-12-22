using multiplixe.comum.dto.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class TwitterOAuthResponse : IUsuarioID, IEmpresaID
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("verifier")]
        public string Verifier { get; set; }

        [JsonPropertyName("username")]
        public string ContaRedeSocial { get; set; }
        
        public Guid UsuarioId { get; set; }
        
        public Guid EmpresaId { get; set; }
    }
}
