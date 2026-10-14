using DGE.Core.Features.SiteSections.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.SiteSections.Queries.RequestModels
{
    public class GetSiteSectionByKeyQueryRequestModel : IRequest<Response<SiteSectionFullDataDto>>
    {
        public string Key { get; set; } = string.Empty;
    }
}
