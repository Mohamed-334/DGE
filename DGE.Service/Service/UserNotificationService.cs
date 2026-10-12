using DGE.Domain.Entities;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
{
    public class UserNotificationService : BaseService<UserNotification>, IUserNotificationService
    {
        #region Fields
        private readonly IUserNotificationRepository _userNotificationRepository;
        #endregion

        #region Constructor
        public UserNotificationService(IStringLocalizer<AppLocalization> stringLocalizer, IUserNotificationRepository userNotificationRepository, ILogger<BaseService<UserNotification>> logger) : base(userNotificationRepository, stringLocalizer, logger)
        {
            _userNotificationRepository = userNotificationRepository;
        }
        #endregion

        #region Methods
        public async Task<string> MarkNotificationsAsReadAsync(int userId, CancellationToken cancellationToken = default)
        {
            var userNotifications = await _userNotificationRepository
                                        .GetTableNoTracking()
                                        .Where(un => un.UserId == userId && !un.IsRead)
                                        .ToListAsync(cancellationToken);

            foreach (var userNotification in userNotifications)
            {
                userNotification.IsRead = true;
            }
            var trans = _userNotificationRepository.BeginTransaction();
            try
            {
                await _userNotificationRepository.UpdateRangeAsync(userNotifications, cancellationToken);
                await trans.CommitAsync(cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to mark notifications as read for user {UserId}", userId);
                await trans.RollbackAsync(cancellationToken);
                return _stringLocalizer[AppLocalizationKeys.UpdateFailed];
            }
        }
        public async Task<string> MarkNotificationAsReadAsync(int userId, int notificationId, CancellationToken cancellationToken = default)
        {
            var userNotification = await _userNotificationRepository
                .GetTableNoTracking()
                .FirstOrDefaultAsync(un => un.NotificationId == notificationId && un.UserId == userId, cancellationToken);

            if (userNotification == null)
                return _stringLocalizer[AppLocalizationKeys.NotFound];

            userNotification.IsRead = true;

            var result = await EditAsync(userNotification, cancellationToken);
            return result;
        }

        public async Task<List<Notification?>> GetUserNotificationsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var userNotifications = await _userNotificationRepository
                .GetTableNoTracking()
                .Include(un => un.Notification)
                .Where(un => un.UserId == userId)
                .OrderByDescending(un => un.Notification!.CreatedAt)
                .Select(n => n.Notification)
                .ToListAsync(cancellationToken);

            return userNotifications;
        }
        #endregion
    }
}
