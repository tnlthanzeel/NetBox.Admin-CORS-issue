namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceUpdateSpec : Specification<ServiceType>
{
    public ServiceUpdateSpec(Guid serviceTypeId, Guid serviceId)
    {
        Query.Where(e => e.Id == serviceTypeId)
             .Include(i => i.Services.Where(w => w.Id == serviceId))
             .AsTracking();
    }
}
