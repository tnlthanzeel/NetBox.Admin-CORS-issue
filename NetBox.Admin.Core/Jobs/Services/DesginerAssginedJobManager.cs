using NetBox.Admin.Core.Jobs.DTOs;
using NetBox.Admin.Core.Jobs.Interfaces;
using NetBox.Admin.Core.Jobs.Specs;

namespace NetBox.Admin.Core.Jobs.Services;

sealed class DesginerAssginedJobManager : IDesginerAssginedJobManager
{
    private readonly IDesginerJobRepository _desginerJobRepository;

    public DesginerAssginedJobManager(IDesginerJobRepository desginerJobRepository)
    {
        _desginerJobRepository = desginerJobRepository;
    }

    public async Task<ResponseResult<IReadOnlyList<DesignerAssignedJobSummaryDTO>>> GetJobsAssignedToDesigner(Guid designerId, Paginator paginator,
                                                                                                              CancellationToken token)
    {
        var (list, totalCount) = await _desginerJobRepository.GetProjectedListBySpec(paginator, new AssignedJobsByDesignerSpec(designerId),
                                                                                     token);
        return new(list, totalCount);
    }
}
