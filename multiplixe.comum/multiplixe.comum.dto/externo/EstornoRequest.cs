using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.externo
{
    public class EstornoRequest : BaseRequest
    {
        [JsonPropertyName("partnerId")]
        public string partnerId { get; set; }

        [JsonIgnore]
        public Guid ParceiroId
        { 
            get 
            {
                return TryParse(partnerId);
            }
        }

        [JsonPropertyName("transactionId")]
        public string transactionId { get; set; }

        [JsonIgnore]
        public Guid TransacaoId
        {
            get
            {
                return TryParse(transactionId);
            }
        }
    }
}
