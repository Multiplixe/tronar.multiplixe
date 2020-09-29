using multiplixe.twitter.oauth.dtos;
using System.Threading.Tasks;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.twitter.oauth
{
    public class Servico
    {
        public AuthContext authContext { get; }
        private TwitterHttpClient customHttpClient { get; }

        public Servico(
            TwitterHttpClient twitterHttpClient,
            AuthContext twitterAuthContext)
        {
            this.customHttpClient = twitterHttpClient;
            this.authContext = twitterAuthContext;
        }

        public async Task<adduohelper.ResponseEnvelope<string>> Authenticate()
        {

            var querystring = await customHttpClient.QuerystringAuthenticate(
                authContext.UrlApi,
                authContext.ConsumerKey,
                authContext.ConsumerSecret,
                authContext.AccessToken,
                authContext.AccessSecret);

            var url = $"{authContext.UrlApi}/oauth/authenticate?{querystring}";

            var envelope = new adduohelper.ResponseEnvelope<string>(url);

            return envelope;
        }

        public async Task<comum_dto.Perfil> Processar(string token, string verifier)
        {
            var tokenUser = await customHttpClient.Token(
                authContext.UrlApi,
                authContext.ConsumerKey,
                token,
                verifier);

            var accessToken = await customHttpClient.AccessToken(
                authContext.UrlApi,
                authContext.ConsumerKey,
                authContext.ConsumerSecret);

            var user = await customHttpClient.User(
                authContext.UrlApi,
                tokenUser.user_id,
                accessToken);

            var perfil = new comum_dto.Perfil
            {
                PerfilId = tokenUser.user_id,
                Nome = user.name,
                Login = user.screen_name,
                RedeSocial = coreenums.RedeSocialEnum.twitter,
                ImagemUrl = user.profile_image_url_https,
                Token = corehelper.SerializadorHelper.Serializar(tokenUser)
            };

            return perfil;
        }
    }
}
