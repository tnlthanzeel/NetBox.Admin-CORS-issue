using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Specs;

internal sealed class ServiceByIdSpec : Specification<ServiceType, ServiceDto>
{
    public ServiceByIdSpec(Guid serviceTypeId, Guid serviceId)
    {
        Query.Where(w => w.Id == serviceTypeId);

        Query.SelectMany(e => e.Services.Where(w => w.Id == serviceId)
                                        .Select(s => new ServiceDto(s.ServiceTypeId,
                                                                    s.Id,
                                                                    s.Name,
                                                                    s.Rate)));
    }
}
