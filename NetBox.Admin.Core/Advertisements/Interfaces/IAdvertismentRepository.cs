namespace NetBox.Admin.Core.Advertisements.Interfaces;

public interface IAdvertismentRepository : IBaseRepository
{
    Advertisement Add(Advertisement advertisment);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<Advertisement, TResult>
                                                                                          specification, CancellationToken token);

    Task<Advertisement?> GetBySpec(ISpecification<Advertisement> specification);
}
