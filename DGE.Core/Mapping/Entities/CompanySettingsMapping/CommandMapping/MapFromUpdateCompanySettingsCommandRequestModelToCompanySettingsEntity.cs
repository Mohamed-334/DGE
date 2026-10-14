using DGE.Core.Features.CompanySettings.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.CompanySettingsMapping
{
    public partial class CompanySettingsMapping
    {
        #region Methods
        public void MapFromUpdateCompanySettingsCommandRequestModelToCompanySettingsEntity()
        {
            CreateMap<UpdateCompanySettingsCommandRequestModel, CompanySettings>()
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => CleanList(src.Phones)))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => CleanList(src.Emails)))
                .ForMember(dest => dest.NotificationEmail, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.NotificationEmail) ? null : src.NotificationEmail.Trim()))
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateCompanySettingsCommandRequestModel, CompanySettings>>();
        }

        private static List<string> CleanList(List<string>? values) =>
            (values ?? []).Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v.Trim()).Distinct().ToList();
        #endregion
    }
}
