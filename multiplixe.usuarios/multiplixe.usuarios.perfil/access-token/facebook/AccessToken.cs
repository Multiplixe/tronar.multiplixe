using System;

namespace multiplixe.usuarios.perfil.access_token.facebook
{
    public class AccessToken
    {
        public AuthResponse authResponse { get; set; }
    }

    public class AuthResponse
    {
        public int expiresIn { get; set; }
        public string accessToken { get; set; }
        public string graphDomain { get; set; }
        public string grantedScopes { get; set; }
        public string signedRequest { get; set; }
        public int data_access_expiration_time { get; set; }

    }

    public class AccessTokenLongDuration
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public DateTime? expires_in_datetime { get; set; }
    }

}
