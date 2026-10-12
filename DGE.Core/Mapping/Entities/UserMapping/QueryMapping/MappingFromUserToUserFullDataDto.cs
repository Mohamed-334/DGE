using BaseArchitecture.Core.Features.ApplicationUser.DTO;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        public void MappingFromUserToUserFullDataDto()
        {
            CreateMap<User, UserFullDataDto>()
                .ForMember(dest => dest.Role, opt => opt
                        .MapFrom(src => src.UserRoles!.FirstOrDefault()!.Role!.Name))
                .ForMember(dest => dest.RoleId, opt => opt
                        .MapFrom(src => src.UserRoles!.FirstOrDefault()!.RoleId))
                .AfterMap<MetaMappingDataBasedOnSource<User, UserFullDataDto>>();
        }
    }

}
