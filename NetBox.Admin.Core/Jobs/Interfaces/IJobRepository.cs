namespace NetBox.Admin.Core.Jobs.Interfaces;

public interface IJobRepository : IBaseRepository
{
    Job Add(Job job);

    Task<Job?> GetBySpec(ISpecification<Job> specification);
}
