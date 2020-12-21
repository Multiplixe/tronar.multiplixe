namespace multiplixe.comum.dto
{
    public class TwitterCRCResponse
    {
        public string response_token { get; set; }
        public TwitterCRCResponse(string hash)
        {
            response_token = hash;
        }

        public TwitterCRCResponse()
        {

        }
    }
}
