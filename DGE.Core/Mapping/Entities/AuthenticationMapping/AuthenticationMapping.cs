using AutoMapper;

namespace DGE.Core.Mapping.AuthenticationMapping
{
    public partial class AuthenticationMapping : Profile
    {
        #region Constructor
        public AuthenticationMapping()
        {
            MapFromSignupCommandRequestModelToUser();
        }
        #endregion
    }
}
