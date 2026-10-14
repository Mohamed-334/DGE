using AutoMapper;
using DGE.Core.Features.ContactUsMessages.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Domain.Shared.EmailModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DGE.Core.Features.ContactUsMessages.Commands.Handler
{
    public class ContactUsCommandHandler : ResponseHandler, IRequestHandler<AddContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateContactUsCommandRequestModel, Response<string>>,
                                                         IRequestHandler<MarkAsReadContactUsCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IContactUsService _contactUsService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly EmailSettings _emailSettings;
        private readonly ICompanySettingsService _companySettingsService;
        private readonly ILogger<ContactUsCommandHandler> _logger;
        #endregion

        #region Constructor
        public ContactUsCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IContactUsService contactUsService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IBackgroundJobClient backgroundJobClient,
                                  EmailSettings emailSettings,
                                  ICompanySettingsService companySettingsService,
                                  ILogger<ContactUsCommandHandler> logger) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _contactUsService = contactUsService;
            _authenticatedUserService = authenticatedUserService;
            _backgroundJobClient = backgroundJobClient;
            _emailSettings = emailSettings;
            _companySettingsService = companySettingsService;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddContactUsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = _mapper.Map<ContactUs>(request);
            var result = await _contactUsService.AddAsync(ContactUs);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            await NotifyCompanyAsync(ContactUs);
            return Success("", Meta: new { Id = ContactUs.Id });
        }

        public async Task<Response<string>> Handle(MarkAsReadContactUsCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ContactUs = await _contactUsService.GetByIdAsync(request.Id);
            if (ContactUs == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            if (ContactUs.IsRead)
                return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);

            ContactUs.IsRead = true;
            ContactUs.ModificationDate = DateTime.Now;
            ContactUs.ModifierName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _contactUsService.EditAsync(ContactUs);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
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

        // Sent in the background so the visitor does not wait on SMTP; the message is already saved if this fails
        // Inbox order: the dashboard's company settings, then emailSettings.NotificationEmail, then the sender address
        private async Task NotifyCompanyAsync(ContactUs contactUs)
        {
            var to = new[] { (await _companySettingsService.GetAsync())?.NotificationEmail, _emailSettings.NotificationEmail, _emailSettings.FromEmail }
                .FirstOrDefault(email => !string.IsNullOrWhiteSpace(email));
            if (string.IsNullOrWhiteSpace(to))
                return;

            // Visitor input is HTML-encoded so it cannot inject markup or links into the company inbox
            var body = $@"<h3>New contact us message</h3>
<p><b>Name:</b> {WebUtility.HtmlEncode(contactUs.Name)}</p>
<p><b>Email:</b> {WebUtility.HtmlEncode(contactUs.Email)}</p>
<p><b>Subject:</b> {WebUtility.HtmlEncode(contactUs.Subject)}</p>
<p><b>Message:</b></p>
<p style=""white-space: pre-wrap"">{WebUtility.HtmlEncode(contactUs.Message)}</p>
<p><small>Message #{contactUs.Id} received at {contactUs.CreationDate:yyyy-MM-dd HH:mm}</small></p>";
            var subject = $"New contact us message: {contactUs.Subject ?? contactUs.Name}".ReplaceLineEndings(" ");

            try
            {
                _backgroundJobClient.Enqueue<IEmailService>(emailService => emailService.SendEmailAsync(to, body, subject));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to queue the notification email for contact us message {Id}", contactUs.Id);
            }
        }
        #endregion
    }
}
