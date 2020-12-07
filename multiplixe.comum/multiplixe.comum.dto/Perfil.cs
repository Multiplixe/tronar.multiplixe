using multiplixe.comum.dto.interfaces;
using multiplixe.comum.enums;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Perfil : PerfilBasico, IEmpresaID, IUsuarioID
    {
        [JsonPropertyName("companyId")]
        public Guid EmpresaId { get; set; }

        [JsonPropertyName("userId")]
        public Guid UsuarioId { get; set; }

        [JsonPropertyName("socialMedia")]
        public RedeSocialEnum RedeSocial { get; set; }

        [JsonPropertyName("active")]
        public bool Ativo { get; set; }

        [JsonPropertyName("registerAt")]
        public DateTime DataCadastro { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("expiredIn")]
        public DateTime? ExpiracaoToken { get; set; }

        [JsonIgnore]
        public bool ProcessarToken { get; set; }
    }
}


