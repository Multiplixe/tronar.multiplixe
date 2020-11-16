using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Usuario
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonIgnore()]
        public Guid EmpresaId { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("nickname")]
        public string Apelido { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        //public int NivelId { get; set; }

        [JsonIgnore()]
        public DateTime DataCadastro { get; set; }
    }
}
