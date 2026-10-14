using AutoMapper;
using DGE.Core.Features.Banners.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.Banners.Commands.Handler
{
    public class BannerCommandHandler : ResponseHandler, IRequestHandler<AddBannerCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateBannerCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteBannerCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateBannerCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IBannerService _bannerService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public BannerCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IBannerService bannerService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _bannerService = bannerService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddBannerCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Banner = _mapper.Map<Banner>(request);
            if (request.ImageFile != null)
            {
                var imagePath = await _fileService.UploadFileAsync("Banners", request.ImageFile, FileCategoryEnum.Image);
                if (!imagePath.StartsWith("/Images/"))
                    return BadRequest<string>(imagePath);
                Banner.Image = imagePath;
            }
            var result = await _bannerService.AddAsync(Banner);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Banner.Id });
        }

        public async Task<Response<string>> Handle(UpdateBannerCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Banner = await _bannerService.GetByIdAsync(request.Id);
            if (Banner == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var BannerMapper = _mapper.Map(request, Banner);
            if (request.ImageFile != null)
            {
                var imagePath = await _fileService.UploadFileAsync("Banners", request.ImageFile, FileCategoryEnum.Image);
                if (!imagePath.StartsWith("/Images/"))
                    return BadRequest<string>(imagePath);
                BannerMapper.Image = imagePath;
            }
            var result = await _bannerService.EditAsync(BannerMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteBannerCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Banner = await _bannerService.GetByIdAsync(request.Id);
            if (Banner == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _bannerService.HardDeleteAsync(Banner);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateBannerCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Banner = await _bannerService.GetByIdAsync(request.Id);
            if (Banner == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Banner.IsDeleted = !(Banner.IsDeleted);
            Banner.DeletionDate = DateTime.UtcNow;
            Banner.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _bannerService.EditAsync(Banner);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Banner.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
