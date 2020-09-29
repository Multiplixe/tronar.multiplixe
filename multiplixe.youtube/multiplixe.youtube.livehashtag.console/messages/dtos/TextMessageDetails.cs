using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.messages.dtos
{
    public class TextMessageDetails
    {
        [JsonPropertyName("messageText")]
        public string MessageText { get; set; }
    }
}
