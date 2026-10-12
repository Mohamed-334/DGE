using AutoMapper;

namespace DGE.Core.Mapping.ExhibitionMapping
{
    public partial class ExhibitionMapping : Profile
    {
        #region Constructor
        public ExhibitionMapping()
        {
            MapFromAddExhibitionCommandRequestModelToExhibitionEntity();
            MapFromUpdateExhibitionCommandRequestModelToExhibitionEntity();
            MappingFromExhibitionToExhibitionFullDataDto();
            MappingFromExhibitionToDropDown();
        }
        #endregion
    }
}
