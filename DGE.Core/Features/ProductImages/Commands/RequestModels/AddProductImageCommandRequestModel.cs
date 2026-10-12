using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.ProductImages.Commands.RequestModels
{
    public class AddProductImageCommandRequestModel : IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
        public IFormFile? ImagePathFile { get; set; }
    }
}
