using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Infrastructure.Context;
using BaseArchitecture.Infrastructure.RepositoryInterfaces;
using BaseArchitecture.Infrastructure.Shared.BaseRepository;
using BaseArchitecture.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BaseArchitecture.Infrastructure.Repository
{
    public class OtpRepository : BaseRepository<Otp>, IOtpRepository
    {
        #region Fields
        private readonly DbSet<Otp> _Otps;
        private readonly IStringLocalizer<AppLocalization> _localizer;

        #endregion

        #region Methods
        public OtpRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer) : base(context)
        {
            _Otps = context.Set<Otp>();
            _localizer = localizer;
        }
        #endregion

        #region Actions

        #endregion
    }

}
