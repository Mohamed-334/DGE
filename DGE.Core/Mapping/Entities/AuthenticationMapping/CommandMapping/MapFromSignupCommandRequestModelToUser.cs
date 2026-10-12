using DGE.Core.Features.Authentication.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.AuthenticationMapping
{
    public partial class AuthenticationMapping
    {
        #region Methods
        public void MapFromSignupCommandRequestModelToUser()
        {
            CreateMap<SignUpCommandRequestModel, User>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImageUrl))
                .AfterMap<MetaMappingDataBasedOnDestination<SignUpCommandRequestModel, User>>();
        }
        #endregion
    }
}
