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
    public class PackingTypeService : BaseService<PackingType>, IPackingTypeService
    {
        #region Fields
        private readonly IPackingTypeRepository _packingTypeRepository;
        #endregion

        #region Constructor
        public PackingTypeService(IStringLocalizer<AppLocalization> stringLocalizer, IPackingTypeRepository packingTypeRepository, ILogger<BaseService<PackingType>> logger)
            : base(packingTypeRepository, stringLocalizer, logger)
        {
            _packingTypeRepository = packingTypeRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<PackingType>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _packingTypeRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), p => (p.Name != null && p.Name.Contains(search!)) || (p.NameLocalization != null && p.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<List<PackingType>> GetByNamesAsync(IEnumerable<string> names)
        {
            // Lower-cased on both sides so the match ignores case whatever the database collation is
            var nameList = names.Select(n => n.ToLower()).ToList();
            return await _packingTypeRepository.GetTableNoTracking()
                            .Where(x => x.Name != null && nameList.Contains(x.Name.ToLower()))
                            .ToListAsync();
        }
        #endregion
    }
}
