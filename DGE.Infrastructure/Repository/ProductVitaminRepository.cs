using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace DGE.Infrastructure.Repository
{
    public class ProductVitaminRepository : BaseRepository<ProductVitamin>, IProductVitaminRepository
    {
        #region Fields
        private readonly DbSet<ProductVitamin> _productVitamin;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public ProductVitaminRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _productVitamin = context.Set<ProductVitamin>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
