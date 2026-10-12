using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Commands.RequestModels
{
    public class DeletePackingTypeCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
