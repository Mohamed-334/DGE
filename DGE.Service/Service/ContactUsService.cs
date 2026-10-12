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
    public class ContactUsService : BaseService<ContactUs>, IContactUsService
    {
        #region Fields
        private readonly IContactUsRepository _contactUsRepository;
        #endregion

        #region Constructor
        public ContactUsService(IStringLocalizer<AppLocalization> stringLocalizer, IContactUsRepository contactUsRepository, ILogger<BaseService<ContactUs>> logger)
            : base(contactUsRepository, stringLocalizer, logger)
        {
            _contactUsRepository = contactUsRepository;
        }
        #endregion

        #region Methods
        public async Task<PaginatedList<ContactUs>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var Queryable = _contactUsRepository.GetTableNoTracking()
                            .WhereIf(!string.IsNullOrEmpty(search), c => (c.Name != null && c.Name.Contains(search!)))
                            .AsQueryable();

            var PaginatedList = await Queryable
                .ToPaginatedListAsync(pageNumber, pageSize);
            return PaginatedList;
        }
        #endregion
    }
}
