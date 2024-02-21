using Microsoft.EntityFrameworkCore;
using NetBox.Admin.Core.Settings.Services.DTOs;
using NetBox.Admin.Core.Settings.Services.Filters;

namespace NetBox.Admin.Core.Settings.Services.Specs;

sealed class ServiceTypeListSpec : Specification<ServiceType, ServiceTypeDto>
{
    public ServiceTypeListSpec(ServiceTypeFilter filter)
    {

        if (string.IsNullOrWhiteSpace(filter.SearchTerm?.Trim()) is false)
        {
            string? searchTerm = filter.SearchTerm?.Trim();
            Query.Where(w => EF.Functions.Like(w.Name, searchTerm + "%"));
        }

        Query.OrderBy(w => w.Name);

        Query.Select(e => new ServiceTypeDto(e.Id, e.Name, e.Services.Count()));
    }
}
