using System.Net;

namespace adduo.helper.envelopes
{
    public class RequestEnvelope : BaseEnvelope
    {
        public bool Valid { get; set; }
    }
    
    public class RequestEnvelope<T> : RequestEnvelope
    {
        public T Item { get; set; }

        public RequestEnvelope()
        {
        }

        public RequestEnvelope(T _dto)
        {
            Item = _dto;
        }

        public ResponseEnvelope<T> CreateResponse() {

            var response = new ResponseEnvelope<T>();
            response.Item = this.Item;
            response.Culture = this.Culture;
            return response;
        }
    }
}
