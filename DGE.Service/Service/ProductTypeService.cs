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
    public class ProductTypeService : BaseService<ProductType>, IProductTypeService
    {
        #region Fields
        private readonly IProductTypeRepository _productTypeRepository;
        #endregion

        #region Constructor
        public ProductTypeService(IStringLocalizer<AppLocalization> stringLocalizer, IProductTypeRepository productTypeRepository, ILogger<BaseService<ProductType>> logger)
            : base(productTypeRepository, stringLocalizer, logger)
        {
            _productTypeRepository = productTypeRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ProductType>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, int? productId = null, int? typeId = null)
        {
            var Queryable = _productTypeRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Type)
                            .WhereIf(productId.HasValue, p => p.ProductId == productId)
                            .WhereIf(typeId.HasValue, p => p.TypeId == typeId)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<ProductType?> GetByIdWithIncludesAsync(int id)
        {
            return await _productTypeRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Type)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductType>> GetAllWithIncludesAsync()
        {
            return await _productTypeRepository.GetTableNoTracking()
                            .Include(p => p.Product)
                            .Include(p => p.Type)
                            .ToListAsync();
        }
        #endregion
    }
}
