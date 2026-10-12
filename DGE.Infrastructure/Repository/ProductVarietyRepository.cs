using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class ProductVarietyRepository : BaseRepository<ProductVariety>, IProductVarietyRepository
    {
        #region Fields
        private readonly DbSet<ProductVariety> _productVariety;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public ProductVarietyRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _productVariety = context.Set<ProductVariety>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
