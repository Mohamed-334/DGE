using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.PackingTypeMapping
{
    public partial class PackingTypeMapping
    {
        #region Methods
        public void MappingFromPackingTypeToDropDown()
        {
            CreateMap<PackingType, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<PackingType, DropDown>>();
        }
        #endregion
    }
}
