
namespace NetBox.Admin.Core.Settings.JobTypes.Interfaces;

public interface IJobTypeRepository : IBaseRepository
{
    JobType Add(JobType jobType);
    Task<JobType?> GetBySpec(ISpecification<JobType> specification);
    Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<JobType, TResult> specification, CancellationToken token);
    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<JobType, TResult> specification, CancellationToken token);
    Task<IReadOnlyList<TResult>> GetProjectedListBySpec<TResult>(ISpecification<JobType, TResult> specification, CancellationToken token);
}
