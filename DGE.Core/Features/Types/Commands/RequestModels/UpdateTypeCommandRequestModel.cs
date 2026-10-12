using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Types.Commands.RequestModels
{
    public class UpdateTypeCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
    }
}
