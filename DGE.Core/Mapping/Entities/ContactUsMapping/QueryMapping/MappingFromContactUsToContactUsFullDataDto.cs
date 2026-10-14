using DGE.Core.Features.ContactUsMessages.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ContactUsMapping
{
    public partial class ContactUsMapping
    {
        #region Methods
        public void MappingFromContactUsToContactUsFullDataDto()
        {
            CreateMap<ContactUs, ContactUsFullDataDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreationDate))
                .AfterMap<MetaMappingDataBasedOnSource<ContactUs, ContactUsFullDataDto>>();
        }
        #endregion
    }
}
