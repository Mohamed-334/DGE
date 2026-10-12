using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class ProductMineralRepository : BaseRepository<ProductMineral>, IProductMineralRepository
    {
        #region Fields
        private readonly DbSet<ProductMineral> _productMineral;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public ProductMineralRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _productMineral = context.Set<ProductMineral>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
