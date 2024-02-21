using NetBox.Admin.Core.Advertisements;

namespace NetBox.Admin.Core.Customers.Interfaces;
public interface ICustomerRepository : IBaseRepository
{
    Customer Add(Customer customer);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<Customer, TResult>
                                                                                          specification, CancellationToken token);

    Task<Customer?> GetBySpec(ISpecification<Customer> specification);
}
