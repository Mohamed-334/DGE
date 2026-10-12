using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Commands.RequestModels
{
    public class DeletePermissionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
