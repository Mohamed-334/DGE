using DGE.Domain.Entities;
using DGE.Infrastructure.Hubs;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {
        #region Fields
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        #endregion

        #region Constructor
        public NotificationService(IStringLocalizer<AppLocalization> stringLocalizer,
                                   INotificationRepository notificationRepository,
                                   IHubContext<NotificationHub> hubContext,
                                   ILogger<BaseService<Notification>> logger) : base(notificationRepository, stringLocalizer, logger)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }
        #endregion

        #region Methods
        public Task SendNotificationToUsers(Notification notification, List<int> Users)
        {
            return _hubContext.Clients.Users(Users.Select(u => u.ToString()).ToList())
                                .SendAsync("ReceiveNotification", notification, CancellationToken.None);
        }
        public async Task<string> SendNotification(Notification? notification, List<int> Users, CancellationToken cancellationToken = default)
        {
            if (notification == null)
                return _stringLocalizer[AppLocalizationKeys.NotFound];
            var AddResult = await AddAsync(notification, cancellationToken);
            if (AddResult != _stringLocalizer[AppLocalizationKeys.Success])
                return _stringLocalizer[AppLocalizationKeys.AddFailed];

            try
            {
                BackgroundJob.Schedule<NotificationService>(AddResult => AddResult.SendNotificationToUsers(notification, Users), notification.SendAt!.Value);

                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to schedule notification {NotificationId}", notification.Id);
                return _stringLocalizer[AppLocalizationKeys.FailedToSendNotification];
            }

        }
        #endregion
    }
}
