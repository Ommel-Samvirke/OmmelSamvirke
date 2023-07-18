using System.Net;

namespace OmmelSamvirke.API.Middleware.Models;

public class CustomErrorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
