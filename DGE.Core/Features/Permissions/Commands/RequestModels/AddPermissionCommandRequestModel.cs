using BaseArchitecture.Core.Shared.Models;
using MediatR;

namespace BaseArchitecture.Core.Features.Permissions.Commands.RequestModels
{
    public class AddPermissionCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public int PermissionNumber { get; set; }
    }
}
