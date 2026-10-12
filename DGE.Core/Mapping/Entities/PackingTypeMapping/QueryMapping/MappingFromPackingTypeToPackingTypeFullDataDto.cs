using DGE.Core.Features.PackingTypes.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.PackingTypeMapping
{
    public partial class PackingTypeMapping
    {
        #region Methods
        public void MappingFromPackingTypeToPackingTypeFullDataDto()
        {
            CreateMap<PackingType, PackingTypeFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<PackingType, PackingTypeFullDataDto>>();
        }
        #endregion
    }
}
