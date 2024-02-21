using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Settings.Services;

public sealed class ServiceType : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string Name { get; private set; } = null!;

    private readonly List<Service> _services = [];
    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();

    private ServiceType() { }

    public ServiceType(string name)
    {
        Name = name;
    }

    internal void Delete()
    {
        IsDeleted = true;

        foreach (var service in _services)
        {
            service.Deleted();
        }
    }

    internal void AddService(string name, decimal rate)
    {
        _services.Add(new(name, rate));
    }

    internal void Update(string name)
    {
        Name = name;
    }

    internal void DeleteService()
    {
        _services.First().Deleted();
    }

    internal void UpdateService(string name, decimal rate)
    {
        _services.First().Update(name, rate);
    }
}
