using DGE.Core.Features.ApplicationUser.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        public void MappingFromUpdateUserCommandRequestModelToUser()
        {
            CreateMap<UpdateUserCommandRequestQuery, User>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImageUrl))
                .AfterMap<MetaMappingDataBasedOnDestination<UpdateUserCommandRequestQuery, User>>();

        }
    }
}
