using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
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
