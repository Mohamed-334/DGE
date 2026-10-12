using BaseArchitecture.Core.Features.Notifications.Dto;
using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;

namespace BaseArchitecture.Core.Mapping.NotificationMapping
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
