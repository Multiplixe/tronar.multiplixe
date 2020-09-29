using System.Text.Json;

namespace multiplixe.comum.helper
{
    public class SerializadorHelper
    {
        public static string Serializar(object data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}
