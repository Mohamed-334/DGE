using AutoMapper;
using DGE.Core.Features.SiteSections.Dto;
using DGE.Core.Features.SiteSections.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.SiteSections.Queries.Handler
{
    public class SiteSectionHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetSiteSectionByKeyQueryRequestModel, Response<SiteSectionFullDataDto>>
    {
        #region Fields
        private readonly ISiteSectionService _siteSectionService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _siteSectionMapping;
        #endregion

        #region Constructor
        public SiteSectionHandlerQuery(ISiteSectionService siteSectionService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper siteSectionMapping) : base(stringLocalizer)
        {
            _siteSectionService = siteSectionService;
            _stringLocalizer = stringLocalizer;
            _siteSectionMapping = siteSectionMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<SiteSectionFullDataDto>> Handle(GetSiteSectionByKeyQueryRequestModel request, CancellationToken cancellationToken)
        {
            var key = request.Key.Trim().ToLowerInvariant();
            if (!SiteSection.Keys.Contains(key))
                return NotFound<SiteSectionFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            // A known section that was never saved comes back empty so the site can render its placeholder
            var Section = await _siteSectionService.GetByKeyAsync(key);
            var SiteSectionDto = Section == null
                ? new SiteSectionFullDataDto { Key = key }
                : _siteSectionMapping.Map<SiteSectionFullDataDto>(Section);
            return Success(SiteSectionDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
