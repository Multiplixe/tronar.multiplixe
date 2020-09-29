using adduo.helper.envelopes.exeptions;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace adduo.helper.envelopes
{
    public class ResponseEnvelope : BaseEnvelope
    {
        [JsonPropertyName("statusCode")]
        public HttpStatusCode HttpStatusCode { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get { return isSuccess(); } }

        [JsonPropertyName("error")]
        public ErrorEnvelope Error { get; set; }

        public ResponseEnvelope()
        {
            Error = new ErrorEnvelope();
            HttpStatusCode = HttpStatusCode.OK;
        }

        private bool isSuccess()
        {
            return
                HttpStatusCode == HttpStatusCode.OK ||
                HttpStatusCode == HttpStatusCode.Accepted ||
                HttpStatusCode == HttpStatusCode.NoContent ||
                HttpStatusCode == HttpStatusCode.Created;
        }

        public void ThrownIfError()
        {
            if (!Success)
            {
                throw new EnvelopeException(HttpStatusCode);
            }
        }

    }

    public class ResponseEnvelope<T> : ResponseEnvelope
    {
        [JsonPropertyName("item")]
        public T Item { get; set; }


        public ResponseEnvelope(T dto)
        {
            Item = dto;
        }

        public ResponseEnvelope()
        {
            if(!typeof(T).IsPrimitive)
            {
                Item = Activator.CreateInstance<T>();
            }
        }

        public ResponseEnvelope(ResponseEnvelope envelope):this()
        {
            HttpStatusCode = envelope.HttpStatusCode;
            Error = envelope.Error;
            Culture = envelope.Culture;
        }

        public ResponseEnvelope<TClone> Clone<TClone>()
        {
            return new ResponseEnvelope<TClone>
            {
                Culture = this.Culture,
                Error = this.Error
            };
        }
    }

}
