using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.messages.dtos
{
    public class Item
    {
        [JsonPropertyName("snippet")]
        public Snippet Snippet { get; set; }
    }
}
