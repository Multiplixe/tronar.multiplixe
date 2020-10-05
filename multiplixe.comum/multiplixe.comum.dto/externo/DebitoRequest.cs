using multiplixe.comum.dto.interfaces;
using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.externo
{
    public class DebitoRequest: IUsuarioID, IEmpresaID
    {
        [JsonPropertyName("partnerId")]
        public string ParceiroId { get; set; }

        [JsonIgnore]
        public Guid UsuarioId { get; set; }

        [JsonIgnore]
        public Guid EmpresaId { get; set; }

        [JsonPropertyName("points")]
        public int Pontos { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; }

        [JsonPropertyName("partnerTransactionId")]
        public string ParceiroTransacaoId { get; set; }
    }
}
