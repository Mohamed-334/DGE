using DGE.Core.Features.Types.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Core.Mapping.TypeMapping
{
    public partial class TypeMapping
    {
        #region Methods
        public void MappingFromTypeToTypeFullDataDto()
        {
            CreateMap<Type, TypeFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Type, TypeFullDataDto>>();
        }
        #endregion
    }
}
