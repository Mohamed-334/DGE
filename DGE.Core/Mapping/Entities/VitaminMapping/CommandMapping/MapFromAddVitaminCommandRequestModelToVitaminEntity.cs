using DGE.Core.Features.Vitamins.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.VitaminMapping
{
    public partial class VitaminMapping
    {
        #region Methods
        public void MapFromAddVitaminCommandRequestModelToVitaminEntity()
        {
            CreateMap<AddVitaminCommandRequestModel, Vitamin>()
                .ForMember(dest => dest.VitaminUrl, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<AddVitaminCommandRequestModel, Vitamin>>();
        }
        #endregion
    }
}
