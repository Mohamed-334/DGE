using DGE.Core.Mapping.Shared;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Core.Mapping.NotificationMapping
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
