using System.ComponentModel.DataAnnotations;
using B2.App.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace B2.Api.Middleware;

public class B2ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            BadRequestException or ValidationException => (400, "Bad Request"),
            NotFoundException => (404, "Not Found"),
            _ => (500, "Internal Server Error"),
        };

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Status = status,
            Title = title,
            Detail = exception.Message,
        }, cancellationToken);
        
        if (status != 500)
            return true;

        return false;
    }
}