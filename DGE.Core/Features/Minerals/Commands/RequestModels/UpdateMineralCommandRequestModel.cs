using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Minerals.Commands.RequestModels
{
    public class UpdateMineralCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public IFormFile? MineralUrlFile { get; set; }
    }
}
