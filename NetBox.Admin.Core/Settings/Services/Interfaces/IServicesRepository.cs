namespace NetBox.Admin.Core.Settings.Services.Interfaces;

public interface IServicesRepository : IBaseRepository
{
    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator,
                                                                                          ISpecification<Service, TResult> specification,
                                                                                          CancellationToken token);
}
