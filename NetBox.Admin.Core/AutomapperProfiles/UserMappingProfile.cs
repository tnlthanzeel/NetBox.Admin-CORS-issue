using AutoMapper;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Entities;

namespace NetBox.Admin.Core.AutomapperProfiles;

public sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserSummaryDto>()
            .ForMember(d => d.FirstName, s => s.MapFrom(src => src.UserProfile.FirstName))
            .ForMember(d => d.LastName, s => s.MapFrom(src => src.UserProfile.LastName));
    }
}
