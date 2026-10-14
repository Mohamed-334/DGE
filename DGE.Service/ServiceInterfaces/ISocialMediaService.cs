using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using DGE.Service.Shared.PaginatedList;

namespace DGE.Service.ServiceInterfaces
{
    public interface ISocialMediaService : IBaseService<SocialMedia>
    {
        Task<PaginatedList<SocialMedia>> GetPaginatedListWithFilterAsync(int pageNumber = 1, int pageSize = 10, bool? isActive = null);
    }
}
