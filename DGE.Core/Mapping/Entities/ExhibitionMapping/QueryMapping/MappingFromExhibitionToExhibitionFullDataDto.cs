using DGE.Core.Features.Exhibitions.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ExhibitionMapping
{
    public partial class ExhibitionMapping
    {
        #region Methods
        public void MappingFromExhibitionToExhibitionFullDataDto()
        {
            CreateMap<Exhibition, ExhibitionFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Exhibition, ExhibitionFullDataDto>>();
        }
        #endregion
    }
}
