namespace NetBox.Admin.SharedKernal.Exceptions;

public sealed class UnauthorizedException : ApplicationException
{
    internal UnauthorizedException() { }

    public UnauthorizedException(string message) : base(message)
    {
    }
}
