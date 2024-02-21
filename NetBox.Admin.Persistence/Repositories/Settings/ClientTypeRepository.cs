using NetBox.Admin.Core.Settings.ClientTypes;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Settings;

internal sealed class ClientTypeRepository : BaseRepository, IClientTypeRepository
{
    private readonly DbSet<ClientType> _table;

    public ClientTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<ClientType>();
    }

    public ClientType Add(ClientType clientType)
    {
        _table.Add(clientType);

        return clientType;
    }

    public async Task<ClientType?> GetBySpec(ISpecification<ClientType> specification)
    {
        var entity = await _table.WithSpecification(specification).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<ClientType, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);
        return projectedResult;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<ClientType,
                                                                                                       TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }

    public async Task<IReadOnlyList<TResult>> GetProjectedListBySpec<TResult>(ISpecification<ClientType, TResult> specification,
                                                                              CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.ToListAsync(cancellationToken: token);

        return projectedResult;
    }
}
