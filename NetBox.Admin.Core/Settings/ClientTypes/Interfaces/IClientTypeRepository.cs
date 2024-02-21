namespace NetBox.Admin.Core.Settings.ClientTypes.Interfaces;

public interface IClientTypeRepository : IBaseRepository
{
    ClientType Add(ClientType clientType);

    Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<ClientType, TResult> specification, CancellationToken token);

    Task<ClientType?> GetBySpec(ISpecification<ClientType> specification);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<ClientType, TResult>
                                                                                          specification, CancellationToken token);

    Task<IReadOnlyList<TResult>> GetProjectedListBySpec<TResult>(ISpecification<ClientType, TResult> specification, CancellationToken token);
}
