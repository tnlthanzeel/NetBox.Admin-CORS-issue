namespace NetBox.Admin.SharedKernal.Exceptions;

public sealed class BadRequestException : ApplicationException
{
    public string? PropertyName;

    internal BadRequestException() { }

    public BadRequestException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}