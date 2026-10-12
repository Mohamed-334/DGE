using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Types.Commands.RequestModels
{
    public class SoftDeleteAndActivateTypeCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
