using System;
using System.Net;

namespace QuinaNadalClient
{
    public class ApiCallException : Exception
    {
        public HttpStatusCode ResponseStatusCode { get; set; } = HttpStatusCode.OK;

        public ApiCallException(string message) : base(message) { }

        public ApiCallException(string message, Exception innerException) : base(message, innerException) { }

        public ApiCallException(HttpStatusCode responseStatusCode, string responseStatusDescripction) : base($"{(int)responseStatusCode} [{responseStatusDescripction}]")
        {
            ResponseStatusCode = responseStatusCode;
        }
    }
}