using NetBox.Admin.SharedKernal.Exceptions;

namespace NetBox.Admin.SharedKernal.Responses;

public abstract class BaseResponse
{
    protected static BadRequestException _badRequestException = new();
    protected static NotFoundException _notFoundException = new();
    protected static OperationFailedException _operationFailedException = new();
    protected static UnauthorizedException _unauthorizedException = new();
    protected static ApplicationException _applicationException = new();

    public bool Success { get; protected init; }

    public virtual List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = [];

    public BaseResponse()
    {
        Success = false;
    }
}
