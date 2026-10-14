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
        public async Task<PaginatedList<Product>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null, int? productCategoryId = null, bool? showOnHome = null)
        {
            var Queryable = _productRepository.GetTableNoTracking()
                            .Include(p => p.ProductCategory)
                            .Include(p => p.ProductImages)
                            .WhereIf(!string.IsNullOrEmpty(search), p => (p.Name != null && p.Name.Contains(search!)) || (p.NameLocalization != null && p.NameLocalization.Contains(search!)))
                            .WhereIf(productCategoryId.HasValue, p => p.ProductCategoryId == productCategoryId)
                            .WhereIf(showOnHome.HasValue, p => p.ShowOnHome == showOnHome)
                            .OrderBy(p => p.DisplayOrder)
                            .ThenBy(p => p.Id)
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }

        public async Task<Product?> GetByIdWithIncludesAsync(int id)
        {
            return await WithAllDetails().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> GetBySlugWithIncludesAsync(string slug)
        {
            return await WithAllDetails().FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<List<Product>> GetAllWithIncludesAsync()
        {
            return await _productRepository.GetTableNoTracking()
                            .Include(p => p.ProductCategory)
                            .OrderBy(p => p.DisplayOrder)
                            .ThenBy(p => p.Id)
                            .ToListAsync();
        }

        public async Task<bool> IsSlugExistAsync(string slug, int? excludeProductId = null)
        {
            return await _productRepository.GetTableNoTracking()
                            .AnyAsync(p => p.Slug == slug && (!excludeProductId.HasValue || p.Id != excludeProductId));
        }

        // Builds a slug from the name and appends -2, -3, ... until it is free; falls back to "product" for names without latin characters
        public async Task<string> GenerateUniqueSlugAsync(string? name, int? excludeProductId = null)
        {
            var baseSlug = SlugGenerator.Generate(name);
            if (string.IsNullOrEmpty(baseSlug))
                baseSlug = "product";

            var slug = baseSlug;
            for (var suffix = 2; await IsSlugExistAsync(slug, excludeProductId); suffix++)
            {
                var suffixText = $"-{suffix}";
                slug = baseSlug[..Math.Min(baseSlug.Length, SlugGenerator.MaxLength - suffixText.Length)].TrimEnd('-') + suffixText;
            }
            return slug;
        }

        private IQueryable<Product> WithAllDetails()
        {
            return _productRepository.GetTableNoTracking()
                            .Include(p => p.ProductCategory)
                            .Include(p => p.ProductImages)
                            .Include(p => p.ProductVarieties)
                            .Include(p => p.ProductTypes!).ThenInclude(pt => pt.Type)
                            .Include(p => p.ProductPackings!).ThenInclude(pp => pp.PackingType)
                            .Include(p => p.ProductVitamins!).ThenInclude(pv => pv.Vitamin)
                            .Include(p => p.ProductMinerals!).ThenInclude(pm => pm.Mineral)
                            .Include(p => p.ProductHarvestMonths)
                            .AsSplitQuery();
        }
        #endregion
    }
}
