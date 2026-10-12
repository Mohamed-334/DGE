using DGE.Core.Features.ApplicationUser.DTO;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ApplicationUser.Queries.RequestModels
{
    public class GetUserByIdQueryRequestModel : IRequest<Response<UserFullDataDto>>
    {
        public int UserId { get; set; }

    }
}
