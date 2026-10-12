using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;

namespace DGE.Service.ServiceInterfaces
{
    public interface IUserNotificationService : IBaseService<UserNotification>
    {
        Task<string> MarkNotificationsAsReadAsync(int userId, CancellationToken cancellationToken = default);
        Task<string> MarkNotificationAsReadAsync(int userId, int notificationId, CancellationToken cancellationToken = default);
        Task<List<Notification?>> GetUserNotificationsAsync(int userId, CancellationToken cancellationToken = default);
    }
}
