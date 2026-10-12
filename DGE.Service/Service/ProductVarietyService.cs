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
    public class ProductVarietyService : BaseService<ProductVariety>, IProductVarietyService
    {
        #region Fields
        private readonly IProductVarietyRepository _productVarietyRepository;
        #endregion

        #region Constructor
        public ProductVarietyService(IStringLocalizer<AppLocalization> stringLocalizer, IProductVarietyRepository productVarietyRepository, ILogger<BaseService<ProductVariety>> logger)
            : base(productVarietyRepository, stringLocalizer, logger)
        {
            _productVarietyRepository = productVarietyRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductVariety>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null, int? productId = null)
        {
            var Queryable = _productVarietyRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .WhereIf(!string.IsNullOrEmpty(search), p => (p.Name != null && p.Name.Contains(search!)) || (p.NameLocalization != null && p.NameLocalization.Contains(search!)))
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductVariety?> GetByIdWithIncludesAsync(int id)
        {
            return await _productVarietyRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductVariety>> GetAllWithIncludesAsync()
        {
            return await _productVarietyRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .ToListAsync();
        }
        #endregion
    }
}
