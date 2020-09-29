using multiplixe.twitter.dto;
using multiplixe.twitter.oauth.dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.twitter.oauth
{
    /// <summary>
    /// Configuração está na startup do proxy api
    /// O nome desta classe deve ser diferente das outras possíveis, por isso o prefixo Twitter
    /// </summary>
    public class TwitterHttpClient
    {
        private HttpClient httpClient { get; }

        public TwitterHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> QuerystringAuthenticate(string url, string consumerKey, string consumerSecret, string accessToken, string accessSecret)
        {
            var requestUri = $"{url}/oauth/request_token";

            var oaut = new comum_dto.oauth.OAuth10Request(requestUri, HttpMethod.Get.Method, consumerKey, consumerSecret, accessToken, accessSecret);

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", oaut.ToString());

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        public async Task<TokenUser> Token(string url, string consumerKey, string token, string verifier)
        {
            var accessTokenDto = new comum_dto.oauth.OAuth10Parametros(consumerKey, token, verifier);

            httpClient.DefaultRequestHeaders.Clear();
            var content = new FormUrlEncodedContent(accessTokenDto.ObterParametros());

            var requestUri = $"{url}/oauth/access_token";

            var response = await httpClient.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            string result = response.Content.ReadAsStringAsync().Result;

            var querystring = HttpUtility.ParseQueryString(result);

            var tokenUser = new TokenUser()
            {
                oauth_token = querystring["oauth_token"],
                oauth_token_secret = querystring["oauth_token_secret"],
                user_id = querystring["user_id"]
            };

            return tokenUser;
        }

        public async Task<comum_dto.oauth.OAuthResponse> AccessToken(string url, string consumerKey, string consumerSecret)
        {
            var concatConsumer = string.Concat(Uri.EscapeDataString(consumerKey), ":", Uri.EscapeDataString(consumerSecret));
            var basicToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(concatConsumer));

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + basicToken);

            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("grant_type", "client_credentials") });

            var response = await httpClient.PostAsync(url + "/oauth2/token", content);

            response.EnsureSuccessStatusCode();

            string result = response.Content.ReadAsStringAsync().Result;

            var accessToken = corehelper.DeserializadorHelper.Deserializar<comum_dto.oauth.OAuthResponse>(result);

            return accessToken;
        }

        public async Task<User> User(string url, string user_id, comum_dto.oauth.OAuthResponse accessToken)
        {
            var requestUri = $"{url}/1.1/users/show.json?include_entities=false&user_id={user_id}";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.access_token}");

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string result = response.Content.ReadAsStringAsync().Result;

            var user = corehelper.DeserializadorHelper.Deserializar<User>(result);

            return user;
        }


    }
}
