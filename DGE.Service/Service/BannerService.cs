using DGE.Domain.Entities;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.BaseService;
using DGE.Service.Shared.ExtensionMethods;
using DGE.Service.Shared.PaginatedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DGE.Service.Service
{
    public class BannerService : BaseService<Banner>, IBannerService
    {
        #region Fields
        private readonly IBannerRepository _bannerRepository;
        #endregion

        #region Constructor
        public BannerService(IStringLocalizer<AppLocalization> stringLocalizer, IBannerRepository bannerRepository, ILogger<BaseService<Banner>> logger)
            : base(bannerRepository, stringLocalizer, logger)
        {
            _bannerRepository = bannerRepository;
        }
        #endregion

        #region Methods
        public override async Task<List<Banner>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _bannerRepository.GetTableNoTracking()
                            .OrderBy(x => x.DisplayOrder)
                            .ThenBy(x => x.Id)
                            .ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<Banner>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, bool? isActive = null)
        {
            var Queryable = _bannerRepository.GetTableNoTracking()
                            .WhereIf(isActive.HasValue, x => x.IsActive == isActive)
                            .OrderBy(x => x.DisplayOrder)
                            .ThenBy(x => x.Id)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }
        #endregion
    }
}
