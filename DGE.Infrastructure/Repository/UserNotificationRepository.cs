using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class UserNotificationRepository : BaseRepository<UserNotification>, IUserNotificationRepository
    {
        #region Fields
        private readonly DbSet<UserNotification> _otp;
        private readonly IStringLocalizer<AppLocalization> _localizer;

        #endregion

        #region Methods
        public UserNotificationRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer) : base(context)
        {
            _otp = context.Set<UserNotification>();
            _localizer = localizer;
        }
        #endregion

        #region Actions

        #endregion
    }
}
