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
    public class ExhibitionService : BaseService<Exhibition>, IExhibitionService
    {
        #region Fields
        private readonly IExhibitionRepository _exhibitionRepository;
        #endregion

        #region Constructor
        public ExhibitionService(IStringLocalizer<AppLocalization> stringLocalizer, IExhibitionRepository exhibitionRepository, ILogger<BaseService<Exhibition>> logger)
            : base(exhibitionRepository, stringLocalizer, logger)
        {
            _exhibitionRepository = exhibitionRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<Exhibition>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _exhibitionRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), e => (e.Name != null && e.Name.Contains(search!)) || (e.NameLocalization != null && e.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }
        #endregion
    }
}
