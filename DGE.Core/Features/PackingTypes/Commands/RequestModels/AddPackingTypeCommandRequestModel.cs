using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Commands.RequestModels
{
    public class AddPackingTypeCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
    }
}
