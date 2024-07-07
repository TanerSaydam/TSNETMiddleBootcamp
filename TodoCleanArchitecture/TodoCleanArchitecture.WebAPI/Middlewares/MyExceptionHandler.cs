using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using TS.Result;

namespace TodoCleanArchitecture.WebAPI.Middlewares;

public sealed class MyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception.GetType() == typeof(ArgumentException))
        {
            httpContext.Response.StatusCode = 400;
        }
        else if (exception.GetType() == typeof(ArgumentNullException))
        {
            httpContext.Response.StatusCode = 404;
        }
        else
        {
            httpContext.Response.StatusCode = 500;
        }

        httpContext.Response.ContentType = "application/json";

        string errorMessage = exception.Message;
        Result<string> error = Result<string>.Failure(errorMessage);
        string errorString = JsonSerializer.Serialize(error);
        await httpContext.Response.WriteAsync(errorString);

        return true;
    }
}
