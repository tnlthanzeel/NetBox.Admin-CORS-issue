using NetBox.Admin.Core.Jobs;
using NetBox.Admin.Core.Jobs.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Jobs;
sealed class JobRepository : BaseRepository, IJobRepository
{
    private readonly DbSet<Job> _jobTable;

    public JobRepository(AppDbContext dbContext) : base(dbContext)
    {
        _jobTable = _dbContext.Set<Job>();
    }

    public Job Add(Job job)
    {
        _jobTable.Add(job);
        return job;
    }

    public async Task<Job?> GetBySpec(ISpecification<Job> specification)
    {
        var entity = await _jobTable.WithSpecification(specification).FirstOrDefaultAsync();
        return entity;
    }
}
