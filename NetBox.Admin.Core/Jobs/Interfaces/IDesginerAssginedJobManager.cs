
using NetBox.Admin.Core.Jobs.DTOs;

namespace NetBox.Admin.Core.Jobs.Interfaces;

public interface IDesginerAssginedJobManager
{
    Task<ResponseResult<IReadOnlyList<DesignerAssignedJobSummaryDTO>>> GetJobsAssignedToDesigner(Guid designerId, Paginator paginator,
                                                                                                 CancellationToken token);
}
