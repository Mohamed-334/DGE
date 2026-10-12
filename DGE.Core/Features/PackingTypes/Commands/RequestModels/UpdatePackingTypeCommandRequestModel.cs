using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Commands.RequestModels
{
    public class UpdatePackingTypeCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
    }
}
