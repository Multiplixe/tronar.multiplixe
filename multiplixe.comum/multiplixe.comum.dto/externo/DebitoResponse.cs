using multiplixe.comum.enums;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.externo
{
    public class DebitoResponse
    {
        [JsonPropertyName("transactionId")]
        public Guid Id { get; set; }

        [JsonPropertyName("error")]
        public string Erro { get; set; }
    }
}
