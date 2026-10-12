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
    public class ProductCategoryService : BaseService<ProductCategory>, IProductCategoryService
    {
        #region Fields
        private readonly IProductCategoryRepository _productCategoryRepository;
        #endregion

        #region Constructor
        public ProductCategoryService(IStringLocalizer<AppLocalization> stringLocalizer, IProductCategoryRepository productCategoryRepository, ILogger<BaseService<ProductCategory>> logger)
            : base(productCategoryRepository, stringLocalizer, logger)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductCategory>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _productCategoryRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), p => (p.Name != null && p.Name.Contains(search!)) || (p.NameLocalization != null && p.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }
        #endregion
    }
}
