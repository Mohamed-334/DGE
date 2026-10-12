using DGE.Core.Features.Vitamins.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.VitaminMapping
{
    public partial class VitaminMapping
    {
        #region Methods
        public void MappingFromVitaminToVitaminFullDataDto()
        {
            CreateMap<Vitamin, VitaminFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Vitamin, VitaminFullDataDto>>();
        }
        #endregion
    }
}
