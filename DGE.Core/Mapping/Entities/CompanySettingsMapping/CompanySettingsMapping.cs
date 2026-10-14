using AutoMapper;

namespace DGE.Core.Mapping.CompanySettingsMapping
{
    public partial class CompanySettingsMapping : Profile
    {
        #region Constructor
        public CompanySettingsMapping()
        {
            MapFromUpdateCompanySettingsCommandRequestModelToCompanySettingsEntity();
            MappingFromCompanySettingsToCompanySettingsFullDataDto();
        }
        #endregion
    }
}
