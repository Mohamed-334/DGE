using DGE.Core.Features.Notifications.Commands.RequestModels;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.NotificationMapping
{
    public partial class NotificationMapping
    {
        #region Methods
        public void MapFromUpdateNotificationCommandRequestModelToNotificationEntity()
        {
            CreateMap<UpdateNotificationCommandRequestModel, Notification>()
            .AfterMap<MetaMappingDataBasedOnDestination<UpdateNotificationCommandRequestModel, Notification>>();
        }
        #endregion
    }
}
