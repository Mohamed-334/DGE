using DGE.Core.Features.Notifications.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.NotificationMapping
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
