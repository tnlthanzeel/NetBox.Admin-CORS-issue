using NetBox.Admin.Core.Settings.Services;
using NetBox.Admin.Core.Settings.Services.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Settings.Services;

internal sealed class ServicesRepository : BaseRepository, IServicesRepository
{
    private readonly DbSet<Service> _table;

    public ServicesRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<Service>();
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<Service, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }
}
