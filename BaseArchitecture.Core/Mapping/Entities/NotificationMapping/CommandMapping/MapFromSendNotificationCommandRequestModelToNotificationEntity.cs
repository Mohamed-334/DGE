using BaseArchitecture.Core.Features.Notifications.Commands.RequestModels;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.NotificationMapping
{
    public partial class NotificationMapping
    {
        #region Methods
        public void MapFromSendNotificationCommandRequestModelToNotificationEntity()
        {
            CreateMap<SendNotificationCommandRequestModel, Notification>()
            .AfterMap<MetaMappingDataBasedOnDestination<SendNotificationCommandRequestModel, Notification>>();
            #endregion
        }
    }
}
