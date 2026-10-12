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
    public class ProductImageService : BaseService<ProductImage>, IProductImageService
    {
        #region Fields
        private readonly IProductImageRepository _productImageRepository;
        #endregion

        #region Constructor
        public ProductImageService(IStringLocalizer<AppLocalization> stringLocalizer, IProductImageRepository productImageRepository, ILogger<BaseService<ProductImage>> logger)
            : base(productImageRepository, stringLocalizer, logger)
        {
            _productImageRepository = productImageRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductImage>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null)
        {
            var Queryable = _productImageRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductImage?> GetByIdWithIncludesAsync(int id)
        {
            return await _productImageRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductImage>> GetAllWithIncludesAsync()
        {
            return await _productImageRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .ToListAsync();
        }
        #endregion
    }
}
