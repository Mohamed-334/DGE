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
    public class ProductMineralService : BaseService<ProductMineral>, IProductMineralService
    {
        #region Fields
        private readonly IProductMineralRepository _productMineralRepository;
        #endregion

        #region Constructor
        public ProductMineralService(IStringLocalizer<AppLocalization> stringLocalizer, IProductMineralRepository productMineralRepository, ILogger<BaseService<ProductMineral>> logger)
            : base(productMineralRepository, stringLocalizer, logger)
        {
            _productMineralRepository = productMineralRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductMineral>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? mineralId = null)
        {
            var Queryable = _productMineralRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Mineral)
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .WhereIf(mineralId.HasValue, p => p.MineralId == mineralId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductMineral?> GetByIdWithIncludesAsync(int id)
        {
            return await _productMineralRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Mineral)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductMineral>> GetAllWithIncludesAsync()
        {
            return await _productMineralRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Mineral)
                            .ToListAsync();
        }
        #endregion
    }
}
