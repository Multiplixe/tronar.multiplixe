using multiplixe.comum.dto.interfaces;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.externo
{
    public class AutenticacaoRequest : IEmpresaID
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Senha { get; set; }

        [JsonPropertyName("partnerid")]
        public string ParceiroId { get; set; }
        
        [JsonIgnore]
        public Guid EmpresaId { get; set; }
    }
}
