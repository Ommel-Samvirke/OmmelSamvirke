using System.Text;
using Ganss.Xss;

namespace OmmelSamvirke.API.Middleware;

public class SanitizationMiddleware
{
    private readonly RequestDelegate _next;

    public SanitizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        HtmlSanitizer sanitizer = new HtmlSanitizer();
        
        string injectedContent = await new StreamReader(context.Request.Body).ReadToEndAsync();
        string sanitizedContent = sanitizer.Sanitize(injectedContent);
        
        byte[] bytes = Encoding.UTF8.GetBytes(sanitizedContent);
        ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
        context.Request.Body = await byteArrayContent.ReadAsStreamAsync();
        
        await _next(context);
    }
}
