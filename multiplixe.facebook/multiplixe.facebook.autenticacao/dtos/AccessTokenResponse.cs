namespace multiplixe.facebook.autenticacao.dtos
{
    public class AccessTokenResponse
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string token_type { get; set; }

        public ErrorResponse error { get; set; }
    }
}
