using multiplixe.comum.dto.interfaces;
using multiplixe.comum.enums;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Perfil : PerfilBasico, IEmpresaID, IUsuarioID
    {
        [JsonPropertyName("empresaId")]
        public Guid EmpresaId { get; set; }

        [JsonPropertyName("usuarioId")]
        public Guid UsuarioId { get; set; }

        [JsonPropertyName("redeSocial")]
        public RedeSocialEnum RedeSocial { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [JsonPropertyName("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

    }
}


