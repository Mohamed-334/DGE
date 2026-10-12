using AutoMapper;

namespace DGE.Core.Mapping.ContactUsMapping
{
    public partial class ContactUsMapping : Profile
    {
        #region Constructor
        public ContactUsMapping()
        {
            MapFromAddContactUsCommandRequestModelToContactUsEntity();
            MapFromUpdateContactUsCommandRequestModelToContactUsEntity();
            MappingFromContactUsToContactUsFullDataDto();
            MappingFromContactUsToDropDown();
        }
        #endregion
    }
}
