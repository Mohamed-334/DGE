using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Products.Commands.RequestModels
{
    public class AddProductCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public int ProductCategoryId { get; set; }
        public bool ShowOnHome { get; set; }
        public int DisplayOrder { get; set; }
        // Generated from Name when left empty
        public string? Slug { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<string>? VarietyNames { get; set; }
        public List<string>? TypeNames { get; set; }
        public List<string>? PackingTypeNames { get; set; }
        public List<int>? VitaminIds { get; set; }
        public List<int>? MineralIds { get; set; }
        public List<int>? HarvestMonthCodes { get; set; }
    }
}
