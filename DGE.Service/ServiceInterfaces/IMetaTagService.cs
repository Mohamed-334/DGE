using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;

namespace DGE.Service.ServiceInterfaces
{
    public interface IMetaTagService : IBaseService<MetaTag>
    {
        Task<MetaTag?> GetByPageAsync(string page, bool asTracking = false);
    }
}
