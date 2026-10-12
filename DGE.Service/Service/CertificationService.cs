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
    public class CertificationService : BaseService<Certification>, ICertificationService
    {
        #region Fields
        private readonly ICertificationRepository _certificationRepository;
        #endregion

        #region Constructor
        public CertificationService(IStringLocalizer<AppLocalization> stringLocalizer, ICertificationRepository certificationRepository, ILogger<BaseService<Certification>> logger)
            : base(certificationRepository, stringLocalizer, logger)
        {
            _certificationRepository = certificationRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<Certification>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _certificationRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), c => (c.Name != null && c.Name.Contains(search!)) || (c.NameLocalization != null && c.NameLocalization.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }
        #endregion
    }
}
