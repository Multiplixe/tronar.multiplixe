using System;
using System.Net;

namespace adduo.helper.envelopes.exeptions
{
    public class EnvelopeException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public EnvelopeException()
        {

        }
        public EnvelopeException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

    }
}
