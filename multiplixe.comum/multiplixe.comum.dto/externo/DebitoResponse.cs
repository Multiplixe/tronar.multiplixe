﻿using System;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.externo
{
    public class DebitoResponse
    {
        [JsonPropertyName("transactionId")]
        public Guid Id { get; set; }
    }
}
