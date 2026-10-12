using AutoMapper;

namespace DGE.Core.Mapping.MineralMapping
{
    public partial class MineralMapping : Profile
    {
        #region Constructor
        public MineralMapping()
        {
            MapFromAddMineralCommandRequestModelToMineralEntity();
            MapFromUpdateMineralCommandRequestModelToMineralEntity();
            MappingFromMineralToMineralFullDataDto();
            MappingFromMineralToDropDown();
        }
        #endregion
    }
}
