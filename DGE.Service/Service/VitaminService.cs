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
    public class VitaminService : BaseService<Vitamin>, IVitaminService
    {
        #region Fields
        private readonly IVitaminRepository _vitaminRepository;
        #endregion

        #region Constructor
        public VitaminService(IStringLocalizer<AppLocalization> stringLocalizer, IVitaminRepository vitaminRepository, ILogger<BaseService<Vitamin>> logger)
            : base(vitaminRepository, stringLocalizer, logger)
        {
            _vitaminRepository = vitaminRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<Vitamin>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _vitaminRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), v => (v.Name != null && v.Name.Contains(search!)) || (v.NameLocalization != null && v.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }
        #endregion
    }
}
