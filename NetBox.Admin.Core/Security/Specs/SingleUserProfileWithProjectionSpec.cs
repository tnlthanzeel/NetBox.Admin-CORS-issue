using Ardalis.Specification;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Entities;

namespace NetBox.Admin.Core.Security.Specs;

public sealed class SingleUserProfileWithProjectionSpec : Specification<ApplicationUser, UserProfileDto>
{
    public SingleUserProfileWithProjectionSpec(Guid userId)
    {
        Query.Where(u => u.Id == userId);

        Query.Select(s => new UserProfileDto()
        {
            Id = s.Id,
            Email = s.Email!,
            UserName = s.UserName!,
            FirstName = s.UserProfile.FirstName,
            LastName = s.UserProfile.LastName,
            TimeZone = s.UserProfile.TimeZone,
            MobileNumber = s.UserProfile.MobileNumber,
            NICNumber = s.UserProfile.NICNumber,
            DisplayName = s.UserProfile.DisplayName,
        });
    }
}
