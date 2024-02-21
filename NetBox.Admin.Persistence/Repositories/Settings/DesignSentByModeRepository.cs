using NetBox.Admin.Core.Settings.DesignSentByModes;
using NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Settings;

internal sealed class DesignSentByModeRepository : BaseRepository, IDesignSentByModeRepository
{
    private readonly DbSet<DesignSentByMode> _table;

    public DesignSentByModeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<DesignSentByMode>();
    }

    public DesignSentByMode Add(DesignSentByMode entity)
    {
        _table.Add(entity);
        return entity;
    }

    public async Task<DesignSentByMode?> GetBySpec(ISpecification<DesignSentByMode> specification)
    {
        var entity = await _table.WithSpecification(specification).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<DesignSentByMode, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);
        return projectedResult;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>
                                                                                             (Paginator paginator,
                                                                                              ISpecification<DesignSentByMode, TResult> specification,
                                                                                              CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }

    public async Task<IReadOnlyList<TResult>> GetProjectedListBySpec<TResult>(ISpecification<DesignSentByMode, TResult> specification,
                                                                              CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.ToListAsync(cancellationToken: token);

        return projectedResult;
    }
}
