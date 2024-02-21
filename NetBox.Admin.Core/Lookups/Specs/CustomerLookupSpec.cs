using NetBox.Admin.Core.Customers;
using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Lookups.Filters;
using NetBox.Admin.Core.Settings.JobTypes;

namespace NetBox.Admin.Core.Lookups.Specs;

sealed class CustomerLookupSpec : Specification<Customer, CustomerLookupDTO>
{
    public CustomerLookupSpec(CustomerLookupFilter filter)
    {
        Query.Where(w => EF.Functions.Like(w.PhoneNumber, filter.searchValue + "%") ||
                         EF.Functions.Like(w.Name, filter.searchValue + "%"))
             .OrderBy(o => o.Name);

        Query.Select(s => new CustomerLookupDTO(s.Id, s.PhoneNumber, s.Name));
    }
}
