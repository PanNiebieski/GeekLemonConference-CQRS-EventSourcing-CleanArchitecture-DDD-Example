using System.Net.Mime;
using System.Text;

public class MethodFailure : IResult
{
    private string message;

    public MethodFailure(string message)
    {
        this.message = message;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = MediaTypeNames.Text.Html;
        httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(message);

        httpContext.Response.StatusCode = 420;
        await httpContext.Response.WriteAsync(message);
    }
}

