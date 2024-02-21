using NetBox.Admin.Core.Customers;
using NetBox.Admin.Core.Customers.Interfaces;

namespace NetBox.Admin.Persistence.Repositories;

sealed class CustomerRepository : BaseRepository, ICustomerRepository
{
    private readonly DbSet<Customer> _customerTable;

    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
        _customerTable = dbContext.Set<Customer>();
    }

    public Customer Add(Customer customer)
    {
        _customerTable.Add(customer);
        return customer;
    }

    public async Task<Customer?> GetBySpec(ISpecification<Customer> specification)
    {
        var entity = await _customerTable.WithSpecification(specification).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<Customer, TResult> specification, CancellationToken token)
    {
        var query = _customerTable.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }
}
