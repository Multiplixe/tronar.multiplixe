namespace multiplixe.facebook.autenticacao.dtos
{
    public class ErrorResponse
    {
        public string message { get; set; }
        public string type { get; set; }
        public int code { get; set; }
        public int error_subcode { get; set; }
    }

}
