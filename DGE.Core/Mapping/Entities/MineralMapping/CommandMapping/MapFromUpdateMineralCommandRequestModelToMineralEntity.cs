using DGE.Core.Features.Minerals.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.MineralMapping
{
    public partial class MineralMapping
    {
        #region Methods
        public void MapFromUpdateMineralCommandRequestModelToMineralEntity()
        {
            CreateMap<UpdateMineralCommandRequestModel, Mineral>()
                .ForMember(dest => dest.MineralUrl, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateMineralCommandRequestModel, Mineral>>();
        }
        #endregion
    }
}
