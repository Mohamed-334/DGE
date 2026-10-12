using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.MineralMapping
{
    public partial class MineralMapping
    {
        #region Methods
        public void MappingFromMineralToDropDown()
        {
            CreateMap<Mineral, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Mineral, DropDown>>();
        }
        #endregion
    }
}
