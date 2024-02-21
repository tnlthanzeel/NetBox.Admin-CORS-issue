using NetBox.Admin.Core.Jobs.DTOs;

namespace NetBox.Admin.Core.Jobs.Specs;

sealed class AssignedJobsByDesignerSpec : Specification<DesignerAssignedJob, DesignerAssignedJobSummaryDTO>
{
    public AssignedJobsByDesignerSpec(Guid designerId)
    {
        Query.Where(w => w.AsigneeId == designerId);

        Query.Select(s => new DesignerAssignedJobSummaryDTO(s.AsigneeId));
    }
}
