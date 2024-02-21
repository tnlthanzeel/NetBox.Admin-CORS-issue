using NetBox.Admin.Core.Settings.Services.DTOs;
using NetBox.Admin.Core.Settings.Services.Filters;

namespace NetBox.Admin.Core.Settings.Services.Interfaces;

public interface IServiceTypeService
{
    Task<ResponseResult<ServiceTypeSummaryDto>> CreateServiceType(CreateServiceTypeDto model);
    Task<ResponseResult> DeleteServiceType(Guid serviceTypeId);
    Task<ResponseResult<ServiceTypeSummaryDto>> GetServiceTypeId(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<ServiceTypeDto>>> GetServiceTypes(Paginator paginator, ServiceTypeFilter filter, CancellationToken token);
    Task<ResponseResult> Update(Guid serviceTypeId, UpdateServiceTypeDto model);
}
