using DGE.Core.Features.CompanySettings.Dto;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.CompanySettingsMapping
{
    public partial class CompanySettingsMapping
    {
        #region Methods
        public void MappingFromCompanySettingsToCompanySettingsFullDataDto()
        {
            CreateMap<CompanySettings, CompanySettingsFullDataDto>();
        }
        #endregion
    }
}
