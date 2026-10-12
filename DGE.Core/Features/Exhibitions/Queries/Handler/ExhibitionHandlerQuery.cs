using AutoMapper;
using DGE.Core.Features.Exhibitions.Dto;
using DGE.Core.Features.Exhibitions.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Exhibitions.Queries.Handler
{
    public class ExhibitionHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetExhibitionsListQueryRequestModel, Response<List<ExhibitionFullDataDto>>>,
                                    IRequestHandler<GetExhibitionByIdQueryRequestModel, Response<ExhibitionFullDataDto>>,
                                    IRequestHandler<GetExhibitionsPaginatedListQueryRequestModel, Response<PaginatedList<ExhibitionFullDataDto>>>,
                                    IRequestHandler<GetExhibitionsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IExhibitionService _exhibitionService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _exhibitionMapping;
        #endregion

        #region Constructor
        public ExhibitionHandlerQuery(IExhibitionService exhibitionService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper exhibitionMapping) : base(stringLocalizer)
        {
            _exhibitionService = exhibitionService;
            _stringLocalizer = stringLocalizer;
            _exhibitionMapping = exhibitionMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ExhibitionFullDataDto>>> Handle(GetExhibitionsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibitions = await _exhibitionService.GetAllAsync();
            if (Exhibitions == null)
                return NotFound<List<ExhibitionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ExhibitionsDto = _exhibitionMapping.Map<List<ExhibitionFullDataDto>>(Exhibitions).ToList();
            return Success(ExhibitionsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ExhibitionsDto.Count });
        }

        public async Task<Response<ExhibitionFullDataDto>> Handle(GetExhibitionByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibition = await _exhibitionService.GetByIdAsync(request.Id);
            if (Exhibition == null)
                return NotFound<ExhibitionFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ExhibitionDto = _exhibitionMapping.Map<ExhibitionFullDataDto>(Exhibition);
            return Success(ExhibitionDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ExhibitionFullDataDto>>> Handle(GetExhibitionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _exhibitionService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ExhibitionFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ExhibitionFullDataDtoList = _exhibitionMapping.Map<List<ExhibitionFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ExhibitionFullDataDto>.Success(ExhibitionFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetExhibitionsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibitions = await _exhibitionService.GetAllAsync();
            if (Exhibitions == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _exhibitionMapping.Map<List<DropDown>>(Exhibitions).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
