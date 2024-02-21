using Ardalis.Specification;
using NetBox.Admin.Core.Security.Entities;

namespace NetBox.Admin.Core.Security.Specs;

internal sealed class SingleUserSpec : Specification<ApplicationUser>
{
    public SingleUserSpec()
    {
        Query.Include(i => i.UserProfile);
    }
}
