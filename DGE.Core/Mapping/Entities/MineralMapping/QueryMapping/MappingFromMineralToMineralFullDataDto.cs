using DGE.Core.Features.Minerals.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.MineralMapping
{
    public partial class MineralMapping
    {
        #region Methods
        public void MappingFromMineralToMineralFullDataDto()
        {
            CreateMap<Mineral, MineralFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Mineral, MineralFullDataDto>>();
        }
        #endregion
    }
}
