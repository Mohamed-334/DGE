using AutoMapper;
using DGE.Core.Features.Vitamins.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.Vitamins.Commands.Handler
{
    public class VitaminCommandHandler : ResponseHandler, IRequestHandler<AddVitaminCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateVitaminCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteVitaminCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateVitaminCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IVitaminService _vitaminService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public VitaminCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IVitaminService vitaminService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _vitaminService = vitaminService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamin = _mapper.Map<Vitamin>(request);
            if (request.VitaminUrlFile != null)
                Vitamin.VitaminUrl = await _fileService.UploadFileAsync("Vitamins", request.VitaminUrlFile, FileCategoryEnum.Image);
            var result = await _vitaminService.AddAsync(Vitamin);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Vitamin.Id });
        }

        public async Task<Response<string>> Handle(UpdateVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamin = await _vitaminService.GetByIdAsync(request.Id);
            if (Vitamin == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var VitaminMapper = _mapper.Map(request, Vitamin);
            if (request.VitaminUrlFile != null)
                VitaminMapper.VitaminUrl = await _fileService.UploadFileAsync("Vitamins", request.VitaminUrlFile, FileCategoryEnum.Image);
            var result = await _vitaminService.EditAsync(VitaminMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamin = await _vitaminService.GetByIdAsync(request.Id);
            if (Vitamin == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _vitaminService.HardDeleteAsync(Vitamin);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Vitamin = await _vitaminService.GetByIdAsync(request.Id);
            if (Vitamin == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Vitamin.IsDeleted = !(Vitamin.IsDeleted);
            Vitamin.DeletionDate = DateTime.UtcNow;
            Vitamin.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _vitaminService.EditAsync(Vitamin);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Vitamin.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
