using BaseArchitecture.Core.Features.Notifications.Commands.RequestModels;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.NotificationMapping
{
    public partial class NotificationMapping
    {
        #region Methods
        public void MapFromAddNotificationCommandRequestModelToNotificationEntity()
        {
            CreateMap<AddNotificationCommandRequestModel, Notification>()
            .AfterMap<MetaMappingDataBasedOnDestination<AddNotificationCommandRequestModel, Notification>>();
            #endregion
        }
    }
}
