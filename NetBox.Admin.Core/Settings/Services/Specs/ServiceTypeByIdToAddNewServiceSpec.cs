namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceTypeByIdToAddNewServiceSpec : Specification<ServiceType>
{
    public ServiceTypeByIdToAddNewServiceSpec(Guid serviceTypeId)
    {
        Query.Where(w => w.Id == serviceTypeId)
             .AsTracking();
    }
}
