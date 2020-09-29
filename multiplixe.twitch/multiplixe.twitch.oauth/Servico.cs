using multiplixe.twitch.oauth.dtos;
using System.Linq;
using System.Threading.Tasks;
using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.twitch.oauth
{
    public class Servico
    {
        private AuthContext authContext { get; }
        private TwitchHttpClient customHttpClient { get; }

        public Servico(
            TwitchHttpClient customHttpClient,
            AuthContext authContext)
        {
            this.authContext = authContext;
            this.customHttpClient = customHttpClient;
        }

        public string Autorizar()
        {
            return string.Concat(authContext.UrlAuth, $"/authorize?response_type=code&client_id={authContext.ClientID}&redirect_uri={authContext.RedirectUrl}&scope={authContext.Scope}");
        }

        public async Task<comum_dto.Perfil> Processar(string code)
        {
            var accessToken = await ObterAccessToken(code);

            var perfil = await ObterPerfil(accessToken);

            return perfil;
        }

        public async Task<comum_dto.Perfil> ObterPerfil(comum_dto.oauth.OAuthResponse accessToken)
        {
            var perfil = new comum_dto.Perfil();
            perfil.RedeSocial = enums.RedeSocialEnum.twitch;

            var usersResponse = await customHttpClient.User(authContext.UrlApi, authContext.ClientID, accessToken);

            var user = usersResponse.data.First();

            perfil.PerfilId = user.id;
            perfil.Nome = user.display_name;
            perfil.ImagemUrl = user.profile_image_url;
            perfil.Token = corehelper.SerializadorHelper.Serializar(accessToken);
            perfil.Login = user.login;

            return perfil;
        }

        public async Task<comum_dto.oauth.OAuthResponse> ObterAccessToken(string code)
        {
            var oAuthResponse = await customHttpClient.AccessToken(authContext.UrlAuth, authContext.ClientID, authContext.ClientSecret, code, authContext.RedirectUrl);

            return oAuthResponse;
        }
    }
}
