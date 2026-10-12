using AutoMapper;
using DGE.Core.Features.Vitamins.Dto;
using DGE.Core.Features.Vitamins.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Vitamins.Queries.Handler
{
    public class VitaminHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetVitaminsListQueryRequestModel, Response<List<VitaminFullDataDto>>>,
                                    IRequestHandler<GetVitaminByIdQueryRequestModel, Response<VitaminFullDataDto>>,
                                    IRequestHandler<GetVitaminsPaginatedListQueryRequestModel, Response<PaginatedList<VitaminFullDataDto>>>,
                                    IRequestHandler<GetVitaminsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IVitaminService _vitaminService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _vitaminMapping;
        #endregion

        #region Constructor
        public VitaminHandlerQuery(IVitaminService vitaminService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper vitaminMapping) : base(stringLocalizer)
        {
            _vitaminService = vitaminService;
            _stringLocalizer = stringLocalizer;
            _vitaminMapping = vitaminMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<VitaminFullDataDto>>> Handle(GetVitaminsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamins = await _vitaminService.GetAllAsync();
            if (Vitamins == null)
                return NotFound<List<VitaminFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var VitaminsDto = _vitaminMapping.Map<List<VitaminFullDataDto>>(Vitamins).ToList();
            return Success(VitaminsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = VitaminsDto.Count });
        }

        public async Task<Response<VitaminFullDataDto>> Handle(GetVitaminByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamin = await _vitaminService.GetByIdAsync(request.Id);
            if (Vitamin == null)
                return NotFound<VitaminFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var VitaminDto = _vitaminMapping.Map<VitaminFullDataDto>(Vitamin);
            return Success(VitaminDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<VitaminFullDataDto>>> Handle(GetVitaminsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _vitaminService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<VitaminFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var VitaminFullDataDtoList = _vitaminMapping.Map<List<VitaminFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<VitaminFullDataDto>.Success(VitaminFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetVitaminsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamins = await _vitaminService.GetAllAsync();
            if (Vitamins == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _vitaminMapping.Map<List<DropDown>>(Vitamins).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
