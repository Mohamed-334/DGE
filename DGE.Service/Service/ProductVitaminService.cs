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
    public class ProductVitaminService : BaseService<ProductVitamin>, IProductVitaminService
    {
        #region Fields
        private readonly IProductVitaminRepository _productVitaminRepository;
        #endregion

        #region Constructor
        public ProductVitaminService(IStringLocalizer<AppLocalization> stringLocalizer, IProductVitaminRepository productVitaminRepository, ILogger<BaseService<ProductVitamin>> logger)
            : base(productVitaminRepository, stringLocalizer, logger)
        {
            _productVitaminRepository = productVitaminRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductVitamin>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? vitaminId = null)
        {
            var Queryable = _productVitaminRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Vitamin)
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .WhereIf(vitaminId.HasValue, p => p.VitaminId == vitaminId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductVitamin?> GetByIdWithIncludesAsync(int id)
        {
            return await _productVitaminRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Vitamin)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductVitamin>> GetAllWithIncludesAsync()
        {
            return await _productVitaminRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Vitamin)
                            .ToListAsync();
        }
        #endregion
    }
}
