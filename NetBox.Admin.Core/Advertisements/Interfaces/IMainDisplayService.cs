using NetBox.Admin.Core.Advertisements.DTOs;

namespace NetBox.Admin.Core.Advertisements.Interfaces;

public interface IMainDisplayService
{
    Task<ResponseResult<AdvertisementDto>> Create(CreateAdvertismentDto model);
    Task<ResponseResult> Delete(Guid id);
    Task<ResponseResult<IReadOnlyList<AdvertisementDto>>> GetList(Paginator paginator, CancellationToken token);
}
