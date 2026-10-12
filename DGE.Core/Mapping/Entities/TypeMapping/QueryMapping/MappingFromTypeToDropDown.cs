using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Core.Mapping.TypeMapping
{
    public partial class TypeMapping
    {
        #region Methods
        public void MappingFromTypeToDropDown()
        {
            CreateMap<Type, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Type, DropDown>>();
        }
        #endregion
    }
}
