using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace adduo.helper.entries
{
    public class ListEntry<T> : Entry
    {
        [JsonPropertyName("list")]
        public List<T> List { get; set; }

        [JsonPropertyName("defaultList")]
        public List<T> ListDefault { get; set; }

        public ListEntry()
        {
            List = new List<T>();
            ListDefault = new List<T>();
        }
    }
}
