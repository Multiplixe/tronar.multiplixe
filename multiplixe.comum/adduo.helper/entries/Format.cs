using System.Text.Json.Serialization;

namespace adduo.helper.entries
{
    public abstract class Format : String
    {
        public Format()
        {

        }

        [JsonPropertyName("formatter")]
        public string Formatted { get { return ToFormat(); } }

        public virtual string ToFormat()
        {
            return Value;
        }
    }


}
