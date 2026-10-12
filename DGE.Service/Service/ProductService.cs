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
    public class ProductService : BaseService<Product>, IProductService
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductService(IStringLocalizer<AppLocalization> stringLocalizer, IProductRepository productRepository, ILogger<BaseService<Product>> logger)
            : base(productRepository, stringLocalizer, logger)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<Product>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null, int? productCategoryId = null)
        {
            var Queryable = _productRepository.GetTableNoTracking()
                            .Include(p => p.ProductCategory)
                            .WhereIf(!string.IsNullOrEmpty(search), p => (p.Name != null && p.Name.Contains(search!)) || (p.NameLocalization != null && p.NameLocalization.Contains(search!)))
                            .WhereIf(productCategoryId.HasValue, p => p.ProductCategoryId == productCategoryId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<Product?> GetByIdWithIncludesAsync(int id)
        {
            return await _productRepository.GetTableNoTracking()
                            .Include(p => p.ProductCategory)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAllWithIncludesAsync()
        {
            return await _productRepository.GetTableNoTracking()
                            .Include(p => p.ProductCategory)
                            .ToListAsync();
        }
        #endregion
    }
}
