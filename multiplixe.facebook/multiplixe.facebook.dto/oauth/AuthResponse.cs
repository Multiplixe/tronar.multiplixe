using multiplixe.comum.dto.interfaces;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.facebook.dto.oauth
{
    public class AuthResponse : IUsuarioID, IEmpresaID
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
