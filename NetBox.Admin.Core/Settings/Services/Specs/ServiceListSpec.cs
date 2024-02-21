using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceListSpec : Specification<Service, ServiceDto>
{
    public ServiceListSpec(Guid serviceTypeId)
    {

        Query.Where(w => w.ServiceTypeId == serviceTypeId)
             .OrderBy(a => a.Name);

        Query.Select(a => new ServiceDto(a.ServiceTypeId, a.Id, a.Name, a.Rate));
    }
}
