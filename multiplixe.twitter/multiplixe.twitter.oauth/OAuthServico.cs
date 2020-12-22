using multiplixe.empresas.client;
using System;
using System.Threading.Tasks;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.twitter.oauth
{
    public class OAuthServico
    {
        private readonly EmpresaClient empresaClient;

        private TwitterHttpClient customHttpClient { get; }

        public OAuthServico(
            TwitterHttpClient twitterHttpClient,
            EmpresaClient empresaClient)
        {
            this.customHttpClient = twitterHttpClient;
            this.empresaClient = empresaClient;
        }

        public async Task<adduohelper.ResponseEnvelope<string>> Authenticate(Guid empresaId, string contaRedeSocial)
        {
            var twitterInfoResponse = empresaClient.ObterInfoTwitter(empresaId, contaRedeSocial);
            twitterInfoResponse.ThrownIfError();

            var twitterInfo = twitterInfoResponse.Item;

            var querystring = await customHttpClient.QuerystringAuthenticate(
                twitterInfo.UrlApi,
                twitterInfo.ApiKey,
                twitterInfo.ConsumerSecret,
                twitterInfo.Token,
                twitterInfo.TokenSecret);

            var url = $"{twitterInfo.UrlApi}/oauth/authenticate?{querystring}";

            var envelope = new adduohelper.ResponseEnvelope<string>(url);

            return envelope;
        }

        public async Task<comum_dto.Perfil> ProcessarTokens(string token, string verifier, Guid empresaId, string contaRedeSocial)
        {
            var twitterInfoResponse = empresaClient.ObterInfoTwitter(empresaId, contaRedeSocial);
            twitterInfoResponse.ThrownIfError();

            var twitterInfo = twitterInfoResponse.Item;

            var tokenUser = await customHttpClient.Token(
                twitterInfo.UrlApi,
                twitterInfo.ApiKey,
                token,
                verifier);

            var accessToken = await customHttpClient.AccessToken(
                twitterInfo.UrlApi,
                twitterInfo.ApiKey,
                twitterInfo.ConsumerSecret);

            var user = await customHttpClient.User(
                twitterInfo.UrlApi,
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
