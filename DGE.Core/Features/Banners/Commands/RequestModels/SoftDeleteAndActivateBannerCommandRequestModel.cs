using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Banners.Commands.RequestModels
{
    public class SoftDeleteAndActivateBannerCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
