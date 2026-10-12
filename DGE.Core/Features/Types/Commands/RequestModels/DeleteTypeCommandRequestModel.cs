using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Types.Commands.RequestModels
{
    public class DeleteTypeCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
