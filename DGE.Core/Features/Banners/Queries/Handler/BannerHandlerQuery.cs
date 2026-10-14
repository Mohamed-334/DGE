using AutoMapper;
using DGE.Core.Features.Banners.Dto;
using DGE.Core.Features.Banners.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Banners.Queries.Handler
{
    public class BannerHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetBannersListQueryRequestModel, Response<List<BannerFullDataDto>>>,
                                    IRequestHandler<GetBannerByIdQueryRequestModel, Response<BannerFullDataDto>>,
                                    IRequestHandler<GetBannersPaginatedListQueryRequestModel, Response<PaginatedList<BannerFullDataDto>>>,
                                    IRequestHandler<GetBannersDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IBannerService _bannerService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _bannerMapping;
        #endregion

        #region Constructor
        public BannerHandlerQuery(IBannerService bannerService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper bannerMapping) : base(stringLocalizer)
        {
            _bannerService = bannerService;
            _stringLocalizer = stringLocalizer;
            _bannerMapping = bannerMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<BannerFullDataDto>>> Handle(GetBannersListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Banners = await _bannerService.GetAllAsync();
            if (Banners == null)
                return NotFound<List<BannerFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var BannersDto = _bannerMapping.Map<List<BannerFullDataDto>>(Banners).ToList();
            return Success(BannersDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = BannersDto.Count });
        }

        public async Task<Response<BannerFullDataDto>> Handle(GetBannerByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Banner = await _bannerService.GetByIdAsync(request.Id);
            if (Banner == null)
                return NotFound<BannerFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var BannerDto = _bannerMapping.Map<BannerFullDataDto>(Banner);
            return Success(BannerDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<BannerFullDataDto>>> Handle(GetBannersPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _bannerService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.IsActive);
            if (PaginatedList == null)
                return NotFound<PaginatedList<BannerFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var BannerFullDataDtoList = _bannerMapping.Map<List<BannerFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<BannerFullDataDto>.Success(BannerFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetBannersDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Banners = await _bannerService.GetAllAsync();
            if (Banners == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _bannerMapping.Map<List<DropDown>>(Banners).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
