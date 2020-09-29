using System.Collections.Generic;

namespace multiplixe.comum.dto.oauth
{
    public class OAuth10Parametros
    {
        private List<KeyValuePair<string, string>> parametros { get; }

        public OAuth10Parametros(string consumerKey, string token, string verifier)
        {
            parametros = new List<KeyValuePair<string, string>>();

            parametros.Add(new KeyValuePair<string, string>("oauth_consumer_key", consumerKey));
            parametros.Add(new KeyValuePair<string, string>("oauth_token", token));
            parametros.Add(new KeyValuePair<string, string>("oauth_verifier", verifier));
        }

        public List<KeyValuePair<string, string>> ObterParametros()
        {
            return parametros;
        }
    }
}
