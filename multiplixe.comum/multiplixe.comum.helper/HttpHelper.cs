using System.Net.Http;
using System.Text;

namespace multiplixe.comum.helper
{
    public class HttpHelper
    {
        public static StringContent StringContent(string dado)
        {
            return new StringContent(dado, Encoding.UTF8, "application/json");
        }

    }
}
