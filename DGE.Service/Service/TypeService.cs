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
using Type = DGE.Domain.Entities.Type;

namespace DGE.Service.Service
{
    public class TypeService : BaseService<Type>, ITypeService
    {
        #region Fields
        private readonly ITypeRepository _typeRepository;
        #endregion

        #region Constructor
        public TypeService(IStringLocalizer<AppLocalization> stringLocalizer, ITypeRepository typeRepository, ILogger<BaseService<Type>> logger)
            : base(typeRepository, stringLocalizer, logger)
        {
            _typeRepository = typeRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<Type>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _typeRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), t => (t.Name != null && t.Name.Contains(search!)) || (t.NameLocalization != null && t.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<List<Type>> GetByNamesAsync(IEnumerable<string> names)
        {
            // Lower-cased on both sides so the match ignores case whatever the database collation is
            var nameList = names.Select(n => n.ToLower()).ToList();
            return await _typeRepository.GetTableNoTracking()
                            .Where(x => x.Name != null && nameList.Contains(x.Name.ToLower()))
                            .ToListAsync();
        }
        #endregion
    }
}
