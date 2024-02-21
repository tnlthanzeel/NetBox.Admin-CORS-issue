namespace NetBox.Admin.Core.Settings.Services.Interfaces;

public interface IServiceTypeRepository : IBaseRepository
{
    ServiceType AddServiceType(ServiceType serviceType);

    Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<ServiceType, TResult> specification, CancellationToken token);

    Task<ServiceType?> GetBySpec(ISpecification<ServiceType> specification);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<ServiceType, TResult> specification, CancellationToken token);
}
