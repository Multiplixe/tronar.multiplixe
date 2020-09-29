using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.livebroadcasts.dtos
{
    public class Item
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("snippet")]
        public Snippet Snippet { get; set; }
    }

}
