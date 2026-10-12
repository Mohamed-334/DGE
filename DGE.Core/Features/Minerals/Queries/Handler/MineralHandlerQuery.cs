using AutoMapper;
using DGE.Core.Features.Minerals.Dto;
using DGE.Core.Features.Minerals.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Minerals.Queries.Handler
{
    public class MineralHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetMineralsListQueryRequestModel, Response<List<MineralFullDataDto>>>,
                                    IRequestHandler<GetMineralByIdQueryRequestModel, Response<MineralFullDataDto>>,
                                    IRequestHandler<GetMineralsPaginatedListQueryRequestModel, Response<PaginatedList<MineralFullDataDto>>>,
                                    IRequestHandler<GetMineralsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IMineralService _mineralService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mineralMapping;
        #endregion

        #region Constructor
        public MineralHandlerQuery(IMineralService mineralService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper mineralMapping) : base(stringLocalizer)
        {
            _mineralService = mineralService;
            _stringLocalizer = stringLocalizer;
            _mineralMapping = mineralMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<MineralFullDataDto>>> Handle(GetMineralsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Minerals = await _mineralService.GetAllAsync();
            if (Minerals == null)
                return NotFound<List<MineralFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var MineralsDto = _mineralMapping.Map<List<MineralFullDataDto>>(Minerals).ToList();
            return Success(MineralsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = MineralsDto.Count });
        }

        public async Task<Response<MineralFullDataDto>> Handle(GetMineralByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Mineral = await _mineralService.GetByIdAsync(request.Id);
            if (Mineral == null)
                return NotFound<MineralFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var MineralDto = _mineralMapping.Map<MineralFullDataDto>(Mineral);
            return Success(MineralDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<MineralFullDataDto>>> Handle(GetMineralsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _mineralService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<MineralFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var MineralFullDataDtoList = _mineralMapping.Map<List<MineralFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<MineralFullDataDto>.Success(MineralFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetMineralsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Minerals = await _mineralService.GetAllAsync();
            if (Minerals == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _mineralMapping.Map<List<DropDown>>(Minerals).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
