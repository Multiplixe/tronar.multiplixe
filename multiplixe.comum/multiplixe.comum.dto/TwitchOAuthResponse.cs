using multiplixe.comum.dto.interfaces;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class TwitchOAuthResponse : IUsuarioID, IEmpresaID
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("username")]
        public string ContaRedeSocial { get; set; }

        public Guid UsuarioId { get; set; }

        public Guid EmpresaId { get; set; }
    }
}
