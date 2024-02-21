using NetBox.Admin.Core.Advertisements;
using NetBox.Admin.Core.Advertisements.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Advertisments;

internal sealed class AdvertismentRepository : BaseRepository, IAdvertismentRepository
{
    private readonly DbSet<Advertisement> _table;

    public AdvertismentRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<Advertisement>();
    }

    public Advertisement Add(Advertisement advertisment)
    {
        _table.Add(advertisment);
        return advertisment;
    }

    public async Task<Advertisement?> GetBySpec(ISpecification<Advertisement> specification)
    {
        var entity = await _table.WithSpecification(specification).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator,
                                                                                                       ISpecification<Advertisement, TResult>
                                                                                                                    specification,
                                                                                                       CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }
}
