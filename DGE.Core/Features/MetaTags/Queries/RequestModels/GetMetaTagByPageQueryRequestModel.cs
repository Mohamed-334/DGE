using DGE.Core.Features.MetaTags.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.MetaTags.Queries.RequestModels
{
    public class GetMetaTagByPageQueryRequestModel : IRequest<Response<MetaTagFullDataDto>>
    {
        public string Page { get; set; } = string.Empty;
    }
}
