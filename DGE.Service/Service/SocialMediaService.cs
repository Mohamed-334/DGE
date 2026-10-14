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
    public class SocialMediaService : BaseService<SocialMedia>, ISocialMediaService
    {
        #region Fields
        private readonly ISocialMediaRepository _socialMediaRepository;
        #endregion

        #region Constructor
        public SocialMediaService(IStringLocalizer<AppLocalization> stringLocalizer, ISocialMediaRepository socialMediaRepository, ILogger<BaseService<SocialMedia>> logger)
            : base(socialMediaRepository, stringLocalizer, logger)
        {
            _socialMediaRepository = socialMediaRepository;
        }
        #endregion

        #region Methods
        public override async Task<List<SocialMedia>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _socialMediaRepository.GetTableNoTracking()
                            .OrderBy(x => x.DisplayOrder)
                            .ThenBy(x => x.Id)
                            .ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<SocialMedia>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, bool? isActive = null)
        {
            var Queryable = _socialMediaRepository.GetTableNoTracking()
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
