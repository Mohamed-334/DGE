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
    public class MetaTagService : BaseService<MetaTag>, IMetaTagService
    {
        #region Fields
        private readonly IMetaTagRepository _metaTagRepository;
        #endregion

        #region Constructor
        public MetaTagService(IStringLocalizer<AppLocalization> stringLocalizer, IMetaTagRepository metaTagRepository, ILogger<BaseService<MetaTag>> logger)
            : base(metaTagRepository, stringLocalizer, logger)
        {
            _metaTagRepository = metaTagRepository;
        }
        #endregion

        #region Methods
        public async Task<MetaTag?> GetByPageAsync(string page, bool asTracking = false)
        {
            var queryable = asTracking ? _metaTagRepository.GetTableAsTracking() : _metaTagRepository.GetTableNoTracking();
            return await queryable.FirstOrDefaultAsync(m => m.Page == page);
        }
        #endregion
    }
}
