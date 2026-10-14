using AutoMapper;
using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.SocialMedias.Commands.Handler
{
    public class SocialMediaCommandHandler : ResponseHandler, IRequestHandler<AddSocialMediaCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateSocialMediaCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteSocialMediaCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateSocialMediaCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ISocialMediaService _socialMediaService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public SocialMediaCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  ISocialMediaService socialMediaService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _socialMediaService = socialMediaService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddSocialMediaCommandRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedia = _mapper.Map<SocialMedia>(request);
            var result = await _socialMediaService.AddAsync(SocialMedia);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = SocialMedia.Id });
        }

        public async Task<Response<string>> Handle(UpdateSocialMediaCommandRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedia = await _socialMediaService.GetByIdAsync(request.Id);
            if (SocialMedia == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var SocialMediaMapper = _mapper.Map(request, SocialMedia);
            var result = await _socialMediaService.EditAsync(SocialMediaMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteSocialMediaCommandRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedia = await _socialMediaService.GetByIdAsync(request.Id);
            if (SocialMedia == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _socialMediaService.HardDeleteAsync(SocialMedia);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateSocialMediaCommandRequestModel request, CancellationToken cancellationToken)
        {
            var SocialMedia = await _socialMediaService.GetByIdAsync(request.Id);
            if (SocialMedia == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            SocialMedia.IsDeleted = !(SocialMedia.IsDeleted);
            SocialMedia.DeletionDate = DateTime.UtcNow;
            SocialMedia.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _socialMediaService.EditAsync(SocialMedia);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (SocialMedia.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
