using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;

namespace DGE.Service.ServiceInterfaces
{
    public interface ICompanySettingsService : IBaseService<CompanySettings>
    {
        Task<CompanySettings?> GetAsync(bool asTracking = false);
    }
}
