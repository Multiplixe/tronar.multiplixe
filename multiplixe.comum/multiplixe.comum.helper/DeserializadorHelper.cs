using System.Text.Json;

namespace multiplixe.comum.helper
{
    public class DeserializadorHelper
    {
        public static T Deserializar<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
