using DGE.Core.Features.Exhibitions.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ExhibitionMapping
{
    public partial class ExhibitionMapping
    {
        #region Methods
        public void MapFromUpdateExhibitionCommandRequestModelToExhibitionEntity()
        {
            CreateMap<UpdateExhibitionCommandRequestModel, Exhibition>()
                .ForMember(dest => dest.ExhibitionUrl, opt => opt.Ignore())
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateExhibitionCommandRequestModel, Exhibition>>();
        }
        #endregion
    }
}
