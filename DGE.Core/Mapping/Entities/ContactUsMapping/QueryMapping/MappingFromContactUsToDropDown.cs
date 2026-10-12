using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.ContactUsMapping
{
    public partial class ContactUsMapping
    {
        #region Methods
        public void MappingFromContactUsToDropDown()
        {
            CreateMap<ContactUs, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<ContactUs, DropDown>>();
        }
        #endregion
    }
}
