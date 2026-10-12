using DGE.Core.Features.PackingTypes.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.PackingTypeMapping
{
    public partial class PackingTypeMapping
    {
        #region Methods
        public void MapFromAddPackingTypeCommandRequestModelToPackingTypeEntity()
        {
            CreateMap<AddPackingTypeCommandRequestModel, PackingType>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddPackingTypeCommandRequestModel, PackingType>>();
        }
        #endregion
    }
}
