using DGE.Core.Features.ContactUsMessages.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.ContactUsMapping
{
    public partial class ContactUsMapping
    {
        #region Methods
        public void MapFromUpdateContactUsCommandRequestModelToContactUsEntity()
        {
            CreateMap<UpdateContactUsCommandRequestModel, ContactUs>()
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateContactUsCommandRequestModel, ContactUs>>();
        }
        #endregion
    }
}
