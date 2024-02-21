namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceTypeDeleteSpec : Specification<ServiceType>
{
    public ServiceTypeDeleteSpec(Guid serviceTypeId)
    {
        Query.Where(e => e.Id == serviceTypeId)
             .Include(i => i.Services)
             .AsTracking()
             .AsSplitQuery();
    }
}
