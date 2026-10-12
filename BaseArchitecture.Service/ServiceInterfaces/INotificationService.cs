using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Service.Shared.Interface;

namespace BaseArchitecture.Service.ServiceInterfaces
{
    public interface INotificationService : IBaseService<Notification>
    {
        Task<string> SendNotification(Notification? notification, List<int> Users, CancellationToken cancellationToken = default);
    }
}
