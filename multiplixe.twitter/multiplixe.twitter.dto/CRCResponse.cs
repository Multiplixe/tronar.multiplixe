namespace multiplixe.twitter.dto
{
    public class CRCResponse
    {
        public string response_token { get; set; }
        public CRCResponse(string hash)
        {
            response_token = hash;
        }
    }
}
