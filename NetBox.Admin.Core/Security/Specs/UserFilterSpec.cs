using Ardalis.Specification;
using NetBox.Admin.Core.Security.Entities;
using NetBox.Admin.Core.Security.Filters;
using NetBox.Admin.SharedKernal;
using Microsoft.EntityFrameworkCore;

namespace NetBox.Admin.Core.Security.Specs;

public sealed class UserFilterSpec : Specification<ApplicationUser>
{
    public UserFilterSpec(UserFilter filter)
    {
        Query.Where(u => u.Id != AppConstants.SuperAdmin.SuperUserId);

        Query.OrderBy(s => s.UserName);

        Query.Include(s => s.UserProfile);

        if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
        {
            Query.Where(au => EF.Functions.Like(au.UserProfile.FirstName, "%" + filter.SearchQuery + "%") ||
                              EF.Functions.Like(au.UserProfile.LastName, "%" + filter.SearchQuery + "%") ||
                              EF.Functions.Like(au.UserName!, "%" + filter.SearchQuery + "%") ||
                              EF.Functions.Like(au.Email!, "%" + filter.SearchQuery + "%"));
        }
    }
}
