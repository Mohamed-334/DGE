using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;

namespace DGE.Service.ServiceInterfaces
{
    public interface INotificationService : IBaseService<Notification>
    {
        Task<string> SendNotification(Notification? notification, List<int> Users, CancellationToken cancellationToken = default);
    }
}
