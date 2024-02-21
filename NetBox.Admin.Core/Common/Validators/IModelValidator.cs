using FluentValidation.Results;

namespace NetBox.Admin.Core.Common.Validators;

public interface IModelValidator
{
    Task<ValidationResult> ValidateAsync<TValidator, TRequest>(TRequest request, CancellationToken token = new());
}