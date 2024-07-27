using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Text.Json;

namespace DomainDrivenDesign.WebAPI;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        if (ex.GetType() == typeof(ArgumentNullException))
        {
            context.Response.StatusCode = 428;
        }
        else
        {
            context.Response.StatusCode = 500;
        }

        context.Response.ContentType = MediaTypeNames.Application.Json;

        object result = new { Message = ex.Message };
        string errorMessage = JsonSerializer.Serialize(result);
        await context.Response.WriteAsync(errorMessage);

        return true;
    }
}
