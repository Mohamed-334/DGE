using BaseArchitecture.Core.Mapping.Shared;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;

namespace BaseArchitecture.Core.Mapping.NotificationMapping
{
    public partial class NotificationMapping
    {
        #region Methods
        public void MappingFromNotificationToDropDown()
        {
            CreateMap<Notification, DropDown>()
                .AfterMap<MetaMappingDataBasedOnSource<Notification, DropDown>>();
        }
        #endregion
    }

}
