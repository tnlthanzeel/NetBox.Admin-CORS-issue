using NetBox.Admin.Core.Settings.JobTypes;
using NetBox.Admin.Core.Settings.JobTypes.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Settings;

internal sealed class JobTypeRepository : BaseRepository, IJobTypeRepository
{
    private readonly DbSet<JobType> _table;

    public JobTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<JobType>();
    }

    public JobType Add(JobType jobType)
    {
        _table.Add(jobType);

        return jobType;
    }

    public async Task<JobType?> GetBySpec(ISpecification<JobType> specification)
    {
        var entity = await _table.WithSpecification(specification).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<JobType, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);
        return projectedResult;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<JobType,
                                                                                                       TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }

    public async Task<IReadOnlyList<TResult>> GetProjectedListBySpec<TResult>(ISpecification<JobType, TResult> specification,
                                                                             CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.ToListAsync(cancellationToken: token);

        return projectedResult;
    }
}
