using System.Net;

namespace OmmelSamvirke.API.Exceptions;

public class RequestException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public RequestException(string message, HttpStatusCode statusCode) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}