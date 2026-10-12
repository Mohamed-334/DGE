using AutoMapper;
using DGE.Core.Features.ContactUsMessages.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ContactUsMessages.Commands.Handler
{
    public class ContactUsCommandHandler : ResponseHandler, IRequestHandler<AddContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateContactUsCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IContactUsService _contactUsService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ContactUsCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IContactUsService contactUsService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _contactUsService = contactUsService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddContactUsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = _mapper.Map<ContactUs>(request);
            var result = await _contactUsService.AddAsync(ContactUs);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ContactUs.Id });
        }

        public async Task<Response<string>> Handle(UpdateContactUsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = await _contactUsService.GetByIdAsync(request.Id);
            if (ContactUs == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ContactUsMapper = _mapper.Map(request, ContactUs);
            var result = await _contactUsService.EditAsync(ContactUsMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteContactUsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = await _contactUsService.GetByIdAsync(request.Id);
            if (ContactUs == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _contactUsService.HardDeleteAsync(ContactUs);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateContactUsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = await _contactUsService.GetByIdAsync(request.Id);
            if (ContactUs == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ContactUs.IsDeleted = !(ContactUs.IsDeleted);
            ContactUs.DeletionDate = DateTime.UtcNow;
            ContactUs.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _contactUsService.EditAsync(ContactUs);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ContactUs.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
