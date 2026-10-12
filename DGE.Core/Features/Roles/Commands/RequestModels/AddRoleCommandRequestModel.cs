using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Roles.Commands.RequestModels
{
    public class AddRoleCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
    }
}
