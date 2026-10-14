using AutoMapper;
using DGE.Core.Features.MetaTags.Dto;
using DGE.Core.Features.MetaTags.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.MetaTags.Queries.Handler
{
    public class MetaTagHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetMetaTagByPageQueryRequestModel, Response<MetaTagFullDataDto>>
    {
        #region Fields
        private readonly IMetaTagService _metaTagService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _metaTagMapping;
        #endregion

        #region Constructor
        public MetaTagHandlerQuery(IMetaTagService metaTagService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper metaTagMapping) : base(stringLocalizer)
        {
            _metaTagService = metaTagService;
            _stringLocalizer = stringLocalizer;
            _metaTagMapping = metaTagMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<MetaTagFullDataDto>> Handle(GetMetaTagByPageQueryRequestModel request, CancellationToken cancellationToken)
        {
            var page = request.Page.Trim().ToLowerInvariant();
            if (!MetaTag.Pages.Contains(page))
                return NotFound<MetaTagFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            // A known page without saved tags comes back empty so the site can fall back to its defaults
            var Tag = await _metaTagService.GetByPageAsync(page);
            var MetaTagDto = Tag == null
                ? new MetaTagFullDataDto { Page = page }
                : _metaTagMapping.Map<MetaTagFullDataDto>(Tag);
            return Success(MetaTagDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
