using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Banners.Queries.RequestModels
{
    public class GetBannersDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
