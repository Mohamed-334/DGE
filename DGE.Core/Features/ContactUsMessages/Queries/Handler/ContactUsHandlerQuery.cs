using AutoMapper;
using DGE.Core.Features.ContactUsMessages.Dto;
using DGE.Core.Features.ContactUsMessages.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ContactUsMessages.Queries.Handler
{
    public class ContactUsHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetContactUsMessagesListQueryRequestModel, Response<List<ContactUsFullDataDto>>>,
                                    IRequestHandler<GetContactUsByIdQueryRequestModel, Response<ContactUsFullDataDto>>,
                                    IRequestHandler<GetContactUsMessagesPaginatedListQueryRequestModel, Response<PaginatedList<ContactUsFullDataDto>>>,
                                    IRequestHandler<GetContactUsMessagesDropDownQueryRequestModel, Response<List<DropDown>>>
    {
        #region Fields
        private readonly IContactUsService _contactUsService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _contactUsMapping;
        #endregion

        #region Constructor
        public ContactUsHandlerQuery(IContactUsService contactUsService, IStringLocalizer<AppLocalization> stringLocalizer, IMapper contactUsMapping) : base(stringLocalizer)
        {
            _contactUsService = contactUsService;
            _stringLocalizer = stringLocalizer;
            _contactUsMapping = contactUsMapping;
        }
        #endregion

        #region Methods
        public async Task<Response<List<ContactUsFullDataDto>>> Handle(GetContactUsMessagesListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUsMessages = await _contactUsService.GetAllAsync();
            if (ContactUsMessages == null)
                return NotFound<List<ContactUsFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ContactUsMessagesDto = _contactUsMapping.Map<List<ContactUsFullDataDto>>(ContactUsMessages).ToList();
            return Success(ContactUsMessagesDto, _stringLocalizer[AppLocalizationKeys.Success], new { TotalCount = ContactUsMessagesDto.Count });
        }

        public async Task<Response<ContactUsFullDataDto>> Handle(GetContactUsByIdQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = await _contactUsService.GetByIdAsync(request.Id);
            if (ContactUs == null)
                return NotFound<ContactUsFullDataDto>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ContactUsDto = _contactUsMapping.Map<ContactUsFullDataDto>(ContactUs);
            return Success(ContactUsDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<PaginatedList<ContactUsFullDataDto>>> Handle(GetContactUsMessagesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var PaginatedList = await _contactUsService.GetPaginatedListWithFilterAsync(request.PageNumber, request.PageSize, request.Search);
            if (PaginatedList == null)
                return NotFound<PaginatedList<ContactUsFullDataDto>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var ContactUsFullDataDtoList = _contactUsMapping.Map<List<ContactUsFullDataDto>>(PaginatedList.Data).ToList();
            var paginatedListDto = PaginatedList<ContactUsFullDataDto>.Success(ContactUsFullDataDtoList, PaginatedList.TotalCount, PaginatedList.CurrentPage, PaginatedList.PageSize);
            return Success(paginatedListDto, _stringLocalizer[AppLocalizationKeys.Success]);
        }

        public async Task<Response<List<DropDown>>> Handle(GetContactUsMessagesDropDownQueryRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUsMessages = await _contactUsService.GetAllAsync();
            if (ContactUsMessages == null)
                return NotFound<List<DropDown>>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var DropDowns = _contactUsMapping.Map<List<DropDown>>(ContactUsMessages).ToList();
            return Success(DropDowns, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
