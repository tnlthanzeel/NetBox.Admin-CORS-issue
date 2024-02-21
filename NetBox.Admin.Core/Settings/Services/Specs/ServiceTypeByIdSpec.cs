using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Specs;

sealed class ServiceTypeByIdSpec : Specification<ServiceType, ServiceTypeSummaryDto>
{
    public ServiceTypeByIdSpec(Guid serviceTypeId)
    {
        Query.Where(w => w.Id == serviceTypeId);

        Query.Select(e => new ServiceTypeSummaryDto(e.Id, e.Name));
    }
}
