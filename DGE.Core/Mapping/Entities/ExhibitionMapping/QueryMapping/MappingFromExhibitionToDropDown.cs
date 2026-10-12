using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ExhibitionMapping
{
    public partial class ExhibitionMapping
    {
        #region Methods
        public void MappingFromExhibitionToDropDown()
        {
            CreateMap<Exhibition, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Exhibition, DropDown>>();
        }
        #endregion
    }
}
