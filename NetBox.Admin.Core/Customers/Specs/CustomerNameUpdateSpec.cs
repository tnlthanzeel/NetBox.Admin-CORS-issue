namespace NetBox.Admin.Core.Customers.Specs;
sealed class CustomerNameUpdateSpec : Specification<Customer>
{
    public CustomerNameUpdateSpec(Guid customerId)
    {
        Query.Where(w => w.Id == customerId)
             .AsTracking();
    }
}
