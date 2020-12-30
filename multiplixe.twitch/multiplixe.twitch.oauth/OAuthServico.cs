using multiplixe.twitch.oauth.dtos;
using System.Linq;
using System.Threading.Tasks;
using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;

namespace multiplixe.twitch.oauth
{
    public class OAuthServico
    {
        private readonly EmpresaClient empresaClient;
        private TwitchParams twitchParams { get; }
        private TwitchHttpClient customHttpClient { get; }

        public OAuthServico(
            TwitchHttpClient customHttpClient,
            TwitchParams twitchParams,
            EmpresaClient empresaClient)
        {
            this.twitchParams = twitchParams;
            this.empresaClient = empresaClient;
            this.customHttpClient = customHttpClient;
        }

        public string Authenticate(Guid empresaId, string contaRedeSocial)
        {
            var twitchInfoResponse = empresaClient.ObterInfoTwitch(empresaId, contaRedeSocial);
            twitchInfoResponse.ThrownIfError();

            var redirectUrl = string.Format(twitchParams.RedirectUrl, contaRedeSocial);

            return string.Concat(twitchParams.UrlAuth, $"/authorize?response_type=code&client_id={twitchInfoResponse.Item.ClientId}&redirect_uri={redirectUrl}&scope={twitchParams.Scope}");
        }

        public async Task<comum_dto.Perfil> Processar(string code, Guid empresaId, string contaRedeSocial)
        {
            var accessToken = await ObterAccessToken(code, empresaId,  contaRedeSocial);

            var perfil = await ObterPerfil(accessToken, empresaId, contaRedeSocial);

            return perfil;
        }

        public async Task<comum_dto.Perfil> ObterPerfil(comum_dto.oauth.OAuthResponse accessToken, Guid empresaId, string contaRedeSocial)
        {
            var twitchInfoResponse = empresaClient.ObterInfoTwitch(empresaId, contaRedeSocial);
            twitchInfoResponse.ThrownIfError();

            var twitchInfo = twitchInfoResponse.Item;

            var perfil = new comum_dto.Perfil();
            perfil.RedeSocial = enums.RedeSocialEnum.twitch;

            var usersResponse = await customHttpClient.User(twitchParams.UrlApi, twitchInfo.ClientId, accessToken);

            var user = usersResponse.data.First();

            perfil.PerfilId = user.id;
            perfil.Nome = user.display_name;
            perfil.ImagemUrl = user.profile_image_url;
            perfil.Token = corehelper.SerializadorHelper.Serializar(accessToken);
            perfil.Login = user.login;

            return perfil;
        }

        public async Task<comum_dto.oauth.OAuthResponse> ObterAccessToken(string code, Guid empresaId, string contaRedeSocial)
        {
            var twitchInfoResponse = empresaClient.ObterInfoTwitch(empresaId, contaRedeSocial);
            twitchInfoResponse.ThrownIfError();

            var twitchInfo = twitchInfoResponse.Item;

            var redirectUrl = string.Format(twitchParams.RedirectUrl, contaRedeSocial);

            var oAuthResponse = await customHttpClient.AccessToken(twitchParams.UrlAuth, twitchInfo.ClientId, twitchInfo.ClientSecret, code, redirectUrl);

            return oAuthResponse;
        }
    }
}
