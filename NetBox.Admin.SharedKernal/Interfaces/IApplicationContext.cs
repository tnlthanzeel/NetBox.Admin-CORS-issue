namespace NetBox.Admin.SharedKernal.Interfaces;

public interface IApplicationContext
{
    string BaseUrl { get; }
    string WebRootPath { get; }
}
