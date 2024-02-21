using NetBox.Admin.Core.Settings.ClientTypes.DTOs;
namespace NetBox.Admin.Core.Settings.ClientTypes.Interfaces;

public interface IClientTypeService
{
    Task<ResponseResult<ClientTypeDto>> CreateClientType(CreateClientTypeDto model);
    Task<ResponseResult> DeleteClientType(Guid id);
    Task<ResponseResult<IReadOnlyList<ClientTypeDto>>> GetAllClientTypes(Paginator paginator, CancellationToken token);
    Task<ResponseResult<ClientTypeDto>> GetClientTypeById(Guid id, CancellationToken token);
    Task<ResponseResult> Update(Guid id, UpdateClientTypeDto model);
}
