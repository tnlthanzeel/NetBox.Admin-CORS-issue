using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using NetBox.Admin.SharedKernal.Helpers;
using Serilog;
using System.Net;

namespace NetBox.Admin.Api;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private const string applicationJSONContentType = "application/json";

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        ErrorResponse errorResponse = new();

        int httpStatusCode = StatusCodes.Status500InternalServerError;

        context.Response.ContentType = applicationJSONContentType;

        var result = string.Empty;

        switch (exception)
        {
            case UniqueConstraintException:
                httpStatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Errors.Add(new KeyValuePair<string, IEnumerable<string>>("Duplicate", new[] { "Duplicate value detected" }));
                result = Serializer.Serialize(errorResponse);
                LogError(exception, errorResponse.TraceId);
                break;
            case OperationCanceledException:
                //if client closes the connection
                httpStatusCode = StatusCodes.Status499ClientClosedRequest;
                result = Serializer.Serialize(new ResponseResult<string>("Client closed the connecion"));
                break;
            default:
                httpStatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Errors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.InternalServerError),
                                         new[] { "Something went wrong, please try again" }));

                result = Serializer.Serialize(errorResponse);
                LogError(exception, errorResponse.TraceId);
                break;
        }

        context.Response.StatusCode = httpStatusCode;

        await context.Response.WriteAsync(result, CancellationToken.None);

        return true;
    }

    private static void LogError(Exception exception, string activityId)
    {
        Log.Error("\n{startLine} \n\n Type:\n{exceptionType} \n\n ActivityId:\n{activity}\n\n Message:\n{exceptionMessage} \n\n Stack Trace:\n                              {stackTrace} \n{endLine}\n",
                                new string('-', 150),
                                exception.GetType().FullName,
                                activityId,
                                exception?.InnerException?.Message ?? exception?.Message,
                                exception?.InnerException?.StackTrace ?? exception?.StackTrace,
                                new string('-', 150));
    }
}
