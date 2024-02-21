namespace NetBox.Admin.Api.Controllers;

[ApiController]
public abstract class AppControllerBase : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfulResponse<T>(ResponseResult<T> responseResult)
    {
        return UnsuccessfulResponseHandler(responseResult);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfulResponse(ResponseResult responseResult)
    {
        return UnsuccessfulResponseHandler(responseResult);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private ObjectResult UnsuccessfulResponseHandler<T>(ResponseResult<T> responseResult)
    {
        var groupedErrors = responseResult.Errors.GroupBy(x => x.Key).ToList();

        return BuildResponseResult(responseResult.ApplicationException!, groupedErrors);
    }

    protected ObjectResult UnsuccessfulResponse<T>(KeySetResponseResult<T> responseResult)
    {
        return UnsuccessfulResponseHandler(responseResult);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfulResponse(KeySetResponseResult responseResult)
    {
        return UnsuccessfulResponseHandler(responseResult);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private ObjectResult UnsuccessfulResponseHandler<T>(KeySetResponseResult<T> responseResult)
    {
        var groupedErrors = responseResult.Errors.GroupBy(x => x.Key).ToList();

        return BuildResponseResult(responseResult.ApplicationException!, groupedErrors);
    }

    private ObjectResult BuildResponseResult(ApplicationException exception, List<IGrouping<string, KeyValuePair<string, IEnumerable<string>>>> groupedErrors)
    {
        ErrorResponse errorResponse = new()
        {
            Errors = groupedErrors.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.Key, s.SelectMany(er => er.Value).ToList())).ToList()
        };

        return exception switch
        {
            BadRequestException => BadRequest(errorResponse),
            NotFoundException => NotFound(errorResponse),
            UnauthorizedException => Unauthorized(errorResponse),
            _ => StatusCode(statusCode: StatusCodes.Status500InternalServerError, errorResponse)
        };
    }
}
