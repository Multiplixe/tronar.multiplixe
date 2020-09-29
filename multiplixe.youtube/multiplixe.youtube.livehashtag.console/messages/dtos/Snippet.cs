using System;
using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.messages.dtos
{
    public class Snippet
    {
        [JsonPropertyName("authorChannelId")]
        public string AuthorChannelId { get; set; }

        [JsonPropertyName("textMessageDetails")]
        public TextMessageDetails TextMessageDetails { get; set; }

        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }
    }
}
