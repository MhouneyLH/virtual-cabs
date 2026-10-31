using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GleisGrain.Common.Errors;

public sealed partial class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetails)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problem = exception is DomainException domain
            ? new ProblemDetails
            {
                Status = domain.Error.Status,
                Title = domain.Error.Title,
                Detail = domain.Error.Detail,
                Extensions = { ["code"] = domain.Error.Code },
            }
            : new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
            };

        if (exception is DomainException domainException)
        {
            LogDomainError(logger, domainException.Error.Code);
        }
        else
        {
            LogUnhandled(logger, exception);
        }

        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;

        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problem,
        });
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Domain error {Code}")]
    private static partial void LogDomainError(ILogger logger, string code);

    [LoggerMessage(Level = LogLevel.Error, Message = "Unhandled exception")]
    private static partial void LogUnhandled(ILogger logger, Exception exception);
}
