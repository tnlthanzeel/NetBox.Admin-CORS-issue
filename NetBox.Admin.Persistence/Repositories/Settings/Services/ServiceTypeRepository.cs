using NetBox.Admin.Core.Settings.Services;
using NetBox.Admin.Core.Settings.Services.Interfaces;

namespace NetBox.Admin.Persistence.Repositories.Settings.Services;

internal sealed class ServiceTypeRepository : BaseRepository, IServiceTypeRepository
{
    private readonly DbSet<ServiceType> _table;

    public ServiceTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<ServiceType>();
    }

    public ServiceType AddServiceType(ServiceType serviceType)
    {
        _table.Add(serviceType);

        return serviceType;
    }

    public async Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<ServiceType, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);
        return projectedResult;
    }

    public async Task<ServiceType?> GetBySpec(ISpecification<ServiceType> specification)
    {
        var serviceType = await _table.WithSpecification(specification).FirstOrDefaultAsync();
        return serviceType;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator,
                                                                                                       ISpecification<ServiceType, TResult>
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
