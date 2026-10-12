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
    public class ProductPackingService : BaseService<ProductPacking>, IProductPackingService
    {
        #region Fields
        private readonly IProductPackingRepository _productPackingRepository;
        #endregion

        #region Constructor
        public ProductPackingService(IStringLocalizer<AppLocalization> stringLocalizer, IProductPackingRepository productPackingRepository, ILogger<BaseService<ProductPacking>> logger)
            : base(productPackingRepository, stringLocalizer, logger)
        {
            _productPackingRepository = productPackingRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductPacking>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? packingTypeId = null)
        {
            var Queryable = _productPackingRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.PackingType)
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .WhereIf(packingTypeId.HasValue, p => p.PackingTypeId == packingTypeId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductPacking?> GetByIdWithIncludesAsync(int id)
        {
            return await _productPackingRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.PackingType)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductPacking>> GetAllWithIncludesAsync()
        {
            return await _productPackingRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.PackingType)
                            .ToListAsync();
        }
        #endregion
    }
}
