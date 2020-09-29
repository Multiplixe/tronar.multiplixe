using adduo.helper.entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace adduo.helper.envelopes
{
    
    public class ErrorEnvelope
    {
        [JsonPropertyName("messages")]
        public List<string> Messages { get; set; }

        [JsonPropertyName("entries")]
        public Dictionary<string, string> Entries { get; set; }

        [JsonIgnore()] 
        public Exception Exception { get; set; }

        public ErrorEnvelope()
        {
            Messages = new List<string>();
            Entries = new Dictionary<string, string>();
        }
    }
}
