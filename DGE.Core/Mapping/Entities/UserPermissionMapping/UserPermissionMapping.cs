using AutoMapper;

namespace DGE.Core.Mapping.UserPermissionMapping
{
    public partial class UserPermissionMapping : Profile
    {
        #region Constructor
        public UserPermissionMapping()
        {
            MapFromAddUserPermissionCommandRequestModelToUserPermissionEntity();
            MapFromUpdateUserPermissionCommandRequestModelToUserPermissionEntity();
            MappingFromUserPermissionToUserPermissionFullDataDto();
            MappingFromUserPermissionToDropDown();
        }
        #endregion

    }
}
