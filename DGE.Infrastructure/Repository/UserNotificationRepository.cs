using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.Context;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.BaseRepository;
using BaseArchitecture.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BaseArchitecture.Infrastructure.Repository
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
