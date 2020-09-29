using multiplixe.twitch.oauth.dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.twitch.oauth
{
    /// <summary>
    /// Configuração está na startup do proxy api
    /// O nome desta classe deve ser diferente das outras possíveis, por isso o prefixo Twitch
    /// </summary>
    public class TwitchHttpClient
    {
        private HttpClient httpClient { get; }

        public TwitchHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<comum_dto.oauth.OAuthResponse> AccessToken(string url, string clientID, string clientSecret, string code, string redirectUrl)
        {
            var requestUri = $"{url}/token?client_id={clientID}&client_secret={clientSecret}&code={code}&grant_type=authorization_code&redirect_uri={redirectUrl}";

            httpClient.DefaultRequestHeaders.Clear();
            var response = await httpClient.PostAsync(requestUri, new StringContent(string.Empty));

            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;

            var accessToken = corehelper.DeserializadorHelper.Deserializar<comum_dto.oauth.OAuthResponse>(json);

            return accessToken;
        }

        public async Task<UsersResponse> User(string url, string clientID, comum_dto.oauth.OAuthResponse accessToken)
        {
            var requestUri = string.Concat(url, "/helix/users");

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.access_token}");
            httpClient.DefaultRequestHeaders.Add("client-id", clientID);

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;

            var userResponse = corehelper.DeserializadorHelper.Deserializar<UsersResponse>(json);

            return userResponse;
        }
    }
}
