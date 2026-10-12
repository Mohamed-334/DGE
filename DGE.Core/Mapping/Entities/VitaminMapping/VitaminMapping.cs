using AutoMapper;

namespace DGE.Core.Mapping.VitaminMapping
{
    public partial class VitaminMapping : Profile
    {
        #region Constructor
        public VitaminMapping()
        {
            MapFromAddVitaminCommandRequestModelToVitaminEntity();
            MapFromUpdateVitaminCommandRequestModelToVitaminEntity();
            MappingFromVitaminToVitaminFullDataDto();
            MappingFromVitaminToDropDown();
        }
        #endregion
    }
}
