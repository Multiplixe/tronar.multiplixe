using adduo.helper.hash;
using System.Text.Json.Serialization;

namespace adduo.helper.entries
{
    public class Password : String
    {
        [JsonIgnore()]
        public string Hash
        {
            get
            {
                return HashHelper.HashSHA512(this.Value);
            }
        }

        public Password()  
        {

        }

        public void Clear()
        {
            this.Value = string.Empty;
        }
    }


}
