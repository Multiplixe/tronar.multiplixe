using adduo.helper.entries;

using System;
using System.Text.Json.Serialization;

namespace adduo.helper.envelopes
{
    public class BaseEnvelope
    {
        [JsonPropertyName("culture")]
        public string Culture { get; set; }

        public BaseEnvelope()
        {
        }

    }
}
