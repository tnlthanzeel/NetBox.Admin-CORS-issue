using FluentValidation.Results;
using NetBox.Admin.SharedKernal.Exceptions;
using System.Net;
using System.Text.Json.Serialization;

namespace NetBox.Admin.SharedKernal.Responses;

public class ResponseResult<T> : BaseResponse
{
    [JsonIgnore]
    public ApplicationException? ApplicationException { get; }

    public ResponseResult(T? value, int totalRecordCount = 1) : base()
    {
        Success = value is not null;
        Data = value;
        _totalRecordCount = totalRecordCount;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        ApplicationException = _badRequestException;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public ResponseResult(IList<KeyValuePair<string, IEnumerable<string>>> validationFailures) : base()
    {
        ApplicationException = _badRequestException;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures);
        }
    }

    public ResponseResult(ApplicationException ex) : base()
    {
        Success = false;
        Data = default;

        string[] errorMsg = [ex.Message];

        switch (ex)
        {
            case BadRequestException e:
                ApplicationException = _badRequestException;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName!, errorMsg));
                break;

            case ValidationException e:
                ApplicationException = _badRequestException;
                Errors.AddRange(e.ValdationErrors);
                break;

            case NotFoundException e:
                ApplicationException = _notFoundException;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName!, errorMsg));
                break;

            case OperationFailedException e:
                ApplicationException = _operationFailedException;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName!, errorMsg));
                break;

            case UnauthorizedException:
                ApplicationException = _unauthorizedException;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.Unauthorized), errorMsg));
                break;

            default:
                ApplicationException = _applicationException;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.InternalServerError), errorMsg));
                break;
        };
    }

    private int _totalRecordCount = 1;
    public int TotalRecordCount
    {
        get
        {
            if (Data is null)
            {
                _totalRecordCount = 0;
            }

            return _totalRecordCount;
        }
    }
    public T? Data { get; private set; }

    [JsonIgnore]
    public override List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = [];

}
