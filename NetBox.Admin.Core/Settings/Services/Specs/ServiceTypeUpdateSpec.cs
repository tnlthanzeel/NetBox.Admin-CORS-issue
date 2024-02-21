namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceTypeUpdateSpec : Specification<ServiceType>
{
    public ServiceTypeUpdateSpec(Guid serviceTypeId)
    {
        Query.Where(e => e.Id == serviceTypeId)
             .AsTracking();
    }
}
