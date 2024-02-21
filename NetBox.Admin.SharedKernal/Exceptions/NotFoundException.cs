namespace NetBox.Admin.SharedKernal.Exceptions;

public sealed class NotFoundException : ApplicationException
{
    public string? PropertyName;

    internal NotFoundException() { }

    public NotFoundException(string propertyName, string name, object key)
        : base($"{name} ({key}) is not found")
    {
        PropertyName = propertyName;
    }
}
