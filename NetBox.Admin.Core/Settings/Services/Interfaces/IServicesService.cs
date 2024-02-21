using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Interfaces;

public interface IServicesService
{
    Task<ResponseResult<ServiceDto>> CreateService(Guid serviceTypeId, CreateServiceDto model);
    Task<ResponseResult> DeleteService(Guid serviceTypeId, Guid serviceId);
    Task<ResponseResult<ServiceDto>> GetServiceById(Guid serviceTypeId, Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<ServiceDto>>> GetServices(Guid serviceTypeId, Paginator paginator, CancellationToken token);
    Task<ResponseResult> UpdateService(Guid serviceTypeId, Guid serviceId, UpdateServiceDto model);
}
