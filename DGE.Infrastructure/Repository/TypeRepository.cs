using DGE.Domain.Entities;
using DGE.Infrastructure.Context;
using DGE.Infrastructure.RepositoryInterfaces;
using DGE.Infrastructure.Shared.BaseRepository;
using DGE.Infrastructure.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Infrastructure.Repository
{
    public class TypeRepository : BaseRepository<Type>, ITypeRepository
    {
        #region Fields
        private readonly DbSet<Type> _type;
        private readonly IStringLocalizer<AppLocalization> _localizer;
        #endregion

        #region Constructor
        public TypeRepository(AppDbContext context, IStringLocalizer<AppLocalization> localizer)
            : base(context)
        {
            _type = context.Set<Type>();
            _localizer = localizer;
        }
        #endregion

        #region Actions
        #endregion
    }
}
