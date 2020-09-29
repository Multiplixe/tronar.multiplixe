using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.livebroadcasts.dtos
{
    public class Snippet
    {
        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("liveChatId")]
        public string LiveChatId { get; set; }
    }
}
