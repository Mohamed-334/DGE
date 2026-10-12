using AutoMapper;

namespace DGE.Core.Mapping.PermissionMapping
{
    public partial class PermissionMapping : Profile
    {
        #region Constructor
        public PermissionMapping()
        {
            MapFromAddPermissionCommandRequestModelToPermissionEntity();
            MapFromUpdatePermissionCommandRequestModelToPermissionEntity();
            MappingFromPermissionToPermissionFullDataDto();
            MappingFromPermissionToDropDown();
        }
        #endregion

    }
}
