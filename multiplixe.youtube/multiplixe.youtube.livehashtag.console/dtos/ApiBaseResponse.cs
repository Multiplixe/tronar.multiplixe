using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.dtos
{
    public class ApiBaseResponse 
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }
        
        [JsonPropertyName("etag")]
        public string eTag { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo PageInfo { get; set; }
    }

}
