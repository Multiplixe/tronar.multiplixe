using  multiplixe.youtube.livehashtag.console.dtos;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.messages.dtos
{
    public class Response : ApiBaseResponse
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("nextPageToken")]
        public string NextPageToken { get; set; }

        [JsonPropertyName("offlineAt")]
        public DateTime? OfflineAt { get; set; }
    }
}
