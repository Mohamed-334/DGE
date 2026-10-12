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
    public class ProductHarvestMonthService : BaseService<ProductHarvestMonth>, IProductHarvestMonthService
    {
        #region Fields
        private readonly IProductHarvestMonthRepository _productHarvestMonthRepository;
        #endregion

        #region Constructor
        public ProductHarvestMonthService(IStringLocalizer<AppLocalization> stringLocalizer, IProductHarvestMonthRepository productHarvestMonthRepository, ILogger<BaseService<ProductHarvestMonth>> logger)
            : base(productHarvestMonthRepository, stringLocalizer, logger)
        {
            _productHarvestMonthRepository = productHarvestMonthRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductHarvestMonth>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? monthStatusCode = null)
        {
            var Queryable = _productHarvestMonthRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .WhereIf(monthStatusCode.HasValue, p => p.MonthStatusCode == monthStatusCode)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductHarvestMonth?> GetByIdWithIncludesAsync(int id)
        {
            return await _productHarvestMonthRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductHarvestMonth>> GetAllWithIncludesAsync()
        {
            return await _productHarvestMonthRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .ToListAsync();
        }
        #endregion
    }
}
