
namespace NetBox.Admin.Core.Jobs.Interfaces;

public interface IDesginerJobRepository : IBaseRepository
{
    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<DesignerAssignedJob, TResult> specification, CancellationToken token);
}
