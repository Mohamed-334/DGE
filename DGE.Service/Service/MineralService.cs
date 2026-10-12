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
    public class MineralService : BaseService<Mineral>, IMineralService
    {
        #region Fields
        private readonly IMineralRepository _mineralRepository;
        #endregion

        #region Constructor
        public MineralService(IStringLocalizer<AppLocalization> stringLocalizer, IMineralRepository mineralRepository, ILogger<BaseService<Mineral>> logger)
            : base(mineralRepository, stringLocalizer, logger)
        {
            _mineralRepository = mineralRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<Mineral>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _mineralRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), m => (m.Name != null && m.Name.Contains(search!)) || (m.NameLocalization != null && m.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<bool> AllExistAsync(IEnumerable<int> ids)
        {
            var idList = ids.Distinct().ToList();
            var found = await _mineralRepository.GetTableNoTracking()
                            .CountAsync(x => idList.Contains(x.Id));
            return found == idList.Count;
        }
        #endregion
    }
}
