using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.VitaminMapping
{
    public partial class VitaminMapping
    {
        #region Methods
        public void MappingFromVitaminToDropDown()
        {
            CreateMap<Vitamin, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Vitamin, DropDown>>();
        }
        #endregion
    }
}
