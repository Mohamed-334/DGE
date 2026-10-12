using AutoMapper;
using DGE.Core.Features.Certifications.Dto;
using DGE.Core.Features.Certifications.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Certifications.Queries.Handler
{
    public class CertificationHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetCertificationsListQueryRequestModel, Response<List<CertificationFullDataDto>>>,
                                    IRequestHandler<GetCertificationByIdQueryRequestModel, Response<CertificationFullDataDto>>,
                                    IRequestHandler<GetCertificationsPaginatedListQueryRequestModel, Response<PaginatedList<CertificationFullDataDto>>>,
                                    IRequestHandler<GetCertificationsDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly ICertificationService _certificationService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _certificationMapping;
        #endregion

        #region Constructor
        public CertificationHandlerQuery(ICertificationService certificationService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper certificationMapping) : base(stringLocalizer)
        {
            _certificationService = certificationService;
            _stringLocalizer = stringLocalizer;
            _certificationMapping = certificationMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<CertificationFullDataDto>>> Handle(GetCertificationsListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Certifications = await _certificationService.GetAllAsync();
            if (Certifications == null)
                return NotFound<List<CertificationFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var CertificationsDto = _certificationMapping.Map<List<CertificationFullDataDto>>(Certifications).ToList();
            return Success(CertificationsDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = CertificationsDto.Count });
        }

        public async Task<Response<CertificationFullDataDto>> Handle(GetCertificationByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Certification = await _certificationService.GetByIdAsync(request.Id);
            if (Certification == null)
                return NotFound<CertificationFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var CertificationDto = _certificationMapping.Map<CertificationFullDataDto>(Certification);
            return Success(CertificationDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<CertificationFullDataDto>>> Handle(GetCertificationsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _certificationService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<CertificationFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var CertificationFullDataDtoList = _certificationMapping.Map<List<CertificationFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<CertificationFullDataDto>.Success(CertificationFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetCertificationsDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Certifications = await _certificationService.GetAllAsync();
            if (Certifications == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _certificationMapping.Map<List<DropDown>>(Certifications).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
