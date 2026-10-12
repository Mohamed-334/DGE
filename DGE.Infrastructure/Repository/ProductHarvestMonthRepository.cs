using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class ProductHarvestMonthRepository : BaseRepository<ProductHarvestMonth>, IProductHarvestMonthRepository
    {
        #region Fields
        private readonly DbSet<ProductHarvestMonth> _productHarvestMonth;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public ProductHarvestMonthRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _productHarvestMonth = context.Set<ProductHarvestMonth>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
