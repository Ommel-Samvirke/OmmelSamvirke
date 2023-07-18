using Newtonsoft.Json;
using OmmelSamvirke.API.Exceptions;
using OmmelSamvirke.API.Middleware.Models;

namespace OmmelSamvirke.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (RequestException ex)
        {
            httpContext.Response.StatusCode = (int)ex.StatusCode;
            httpContext.Response.ContentType = "application/json";

            CustomErrorResponse response = new CustomErrorResponse
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            };

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
