using  multiplixe.youtube.livehashtag.console.dtos;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.livebroadcasts.dtos
{
    public class Response : ApiBaseResponse
    {
        [JsonPropertyName("items")]
        public List<Item> Items{ get; set; }
    }
}
