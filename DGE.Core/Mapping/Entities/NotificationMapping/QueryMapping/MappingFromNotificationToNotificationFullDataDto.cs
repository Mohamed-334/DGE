using DGE.Core.Features.Notifications.Dto;
using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;

namespace DGE.Core.Mapping.NotificationMapping
{
    public partial class NotificationMapping
    {
        #region Methods
        public void MappingFromNotificationToNotificationFullDataDto()
        {
            CreateMap<Notification, NotificationFullDataDto>()
                .AfterMap<MetaMappingDataBasedOnSource<Notification, NotificationFullDataDto>>();
        }
        #endregion
    }

}
