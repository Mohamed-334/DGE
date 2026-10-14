using AutoMapper;
using DGE.Core.Features.SocialMedias.Dto;
using DGE.Core.Features.SocialMedias.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.SocialMedias.Queries.Handler
{
    public class SocialMediaHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetSocialMediasListQueryRequestModel, Response<List<SocialMediaFullDataDto>>>,
                                    IRequestHandler<GetSocialMediaByIdQueryRequestModel, Response<SocialMediaFullDataDto>>,
                                    IRequestHandler<GetSocialMediasPaginatedListQueryRequestModel, Response<PaginatedList<SocialMediaFullDataDto>>>,
                                    IRequestHandler<GetSocialMediasDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly ISocialMediaService _socialMediaService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _socialMediaMapping;
        #endregion

        #region Constructor
        public SocialMediaHandlerQuery(ISocialMediaService socialMediaService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper socialMediaMapping) : base(stringLocalizer)
        {
            _socialMediaService = socialMediaService;
            _stringLocalizer = stringLocalizer;
            _socialMediaMapping = socialMediaMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<SocialMediaFullDataDto>>> Handle(GetSocialMediasListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedias = await _socialMediaService.GetAllAsync();
            if (SocialMedias == null)
                return NotFound<List<SocialMediaFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var SocialMediasDto = _socialMediaMapping.Map<List<SocialMediaFullDataDto>>(SocialMedias).ToList();
            return Success(SocialMediasDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = SocialMediasDto.Count });
        }

        public async Task<Response<SocialMediaFullDataDto>> Handle(GetSocialMediaByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedia = await _socialMediaService.GetByIdAsync(request.Id);
            if (SocialMedia == null)
                return NotFound<SocialMediaFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var SocialMediaDto = _socialMediaMapping.Map<SocialMediaFullDataDto>(SocialMedia);
            return Success(SocialMediaDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<SocialMediaFullDataDto>>> Handle(GetSocialMediasPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _socialMediaService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.IsActive);
            if (PaginatedList == null)
                return NotFound<PaginatedList<SocialMediaFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var SocialMediaFullDataDtoList = _socialMediaMapping.Map<List<SocialMediaFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<SocialMediaFullDataDto>.Success(SocialMediaFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetSocialMediasDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedias = await _socialMediaService.GetAllAsync();
            if (SocialMedias == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _socialMediaMapping.Map<List<DropDown>>(SocialMedias).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
