using Microsoft.AspNetCore.Diagnostics;

namespace B2.Api.Middleware;

public class B2ExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}