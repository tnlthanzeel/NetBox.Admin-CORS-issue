using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;

namespace NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;

public interface IDesignSentByModeService
{
    Task<ResponseResult<DesignSentByModeDto>> Create(CreateDesignSentByModeDto model);
    Task<ResponseResult> Delete(Guid id);
    Task<ResponseResult<DesignSentByModeDto>> GetById(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<DesignSentByModeDto>>> GetList(Paginator paginator, CancellationToken token);
    Task<ResponseResult> Update(Guid id, UpdateDesignSentByModeDto model);
}
