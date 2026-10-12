using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Minerals.Commands.RequestModels
{
    public class AddMineralCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public IFormFile? MineralUrlFile { get; set; }
    }
}
