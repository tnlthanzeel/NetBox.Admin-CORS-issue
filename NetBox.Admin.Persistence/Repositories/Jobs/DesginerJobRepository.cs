using NetBox.Admin.Core.Jobs;
using NetBox.Admin.Core.Jobs.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Jobs;

sealed class DesginerJobRepository : BaseRepository, IDesginerJobRepository
{
    private readonly DbSet<DesignerAssignedJob> _designerJobTable;

    public DesginerJobRepository(AppDbContext dbContext) : base(dbContext)
    {
        _designerJobTable = _dbContext.Set<DesignerAssignedJob>();
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator,
                                                                                                      ISpecification<DesignerAssignedJob, TResult>
                                                                                                                     specification,
                                                                                                      CancellationToken token)
    {
        var query = _designerJobTable.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }
}
