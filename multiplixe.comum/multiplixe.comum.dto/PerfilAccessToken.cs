using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class PerfilAccessToken
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }

        [JsonPropertyName("token_type")]
        public string Tipo { get; set; }
        
        [JsonPropertyName("refresh_token")]
        public string Refresh { get; set; }

        [JsonPropertyName("secret_token")]
        public string Secret { get; set; }

        [JsonPropertyName("expired")]
        public DateTime? Expiracao { get; set; }

        [JsonIgnore]
        public string Json { get; set; }

    }
}
