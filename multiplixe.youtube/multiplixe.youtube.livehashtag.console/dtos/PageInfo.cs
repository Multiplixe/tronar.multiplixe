using System.Text.Json.Serialization;

namespace  multiplixe.youtube.livehashtag.console.dtos
{
    public class PageInfo
    {
        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }
        
        [JsonPropertyName("resultsPerPage")]
        public int ResultsPerPage { get; set; }
    }

}
