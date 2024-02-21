namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceDeleteSpec : Specification<ServiceType>
{
    public ServiceDeleteSpec(Guid serviceTypeId, Guid serviceId)
    {
        Query.Where(e => e.Id == serviceTypeId)
             .Include(i => i.Services.Where(w => w.Id == serviceId))
             .AsTracking()
             .AsSplitQuery();
    }
}
