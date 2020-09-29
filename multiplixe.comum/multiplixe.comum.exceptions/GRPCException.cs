using System;
using System.Net;

namespace multiplixe.comum.exceptions
{
    public class GRPCException : Exception
    {
        public GRPCException()
        {

        }

        public GRPCException(string message) : base($"GRPCException {message}")
        {

        }

        public GRPCException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public GRPCException(HttpStatusCode httpStatusCode, string message) : base($"GRPCException {message}")
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class GRPCException<T> : GRPCException
    {
        public GRPCException(string message) : base($"GRPCException {message}")
        {

        }

        public T Item { get; set; }
    }
}
