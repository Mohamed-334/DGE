using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;

namespace DGE.Service.ServiceInterfaces
{
    public interface ISiteSectionService : IBaseService<SiteSection>
    {
        Task<SiteSection?> GetByKeyAsync(string key, bool asTracking = false);
    }
}
