using DGE.Core.Features.Types.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Core.Mapping.TypeMapping
{
    public partial class TypeMapping
    {
        #region Methods
        public void MapFromAddTypeCommandRequestModelToTypeEntity()
        {
            CreateMap<AddTypeCommandRequestModel, Type>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddTypeCommandRequestModel, Type>>();
        }
        #endregion
    }
}
