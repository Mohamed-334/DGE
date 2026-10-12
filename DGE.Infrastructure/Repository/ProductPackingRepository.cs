using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class ProductPackingRepository : BaseRepository<ProductPacking>, IProductPackingRepository
    {
        #region Fields
        private readonly DbSet<ProductPacking> _productPacking;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public ProductPackingRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _productPacking = context.Set<ProductPacking>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
