using DGE.Core.Features.Minerals.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.MineralMapping
{
    public partial class MineralMapping
    {
        #region Methods
        public void MapFromAddMineralCommandRequestModelToMineralEntity()
        {
            CreateMap<AddMineralCommandRequestModel, Mineral>()
                .ForMember(dest => dest.MineralUrl, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddMineralCommandRequestModel, Mineral>>();
        }
        #endregion
    }
}
