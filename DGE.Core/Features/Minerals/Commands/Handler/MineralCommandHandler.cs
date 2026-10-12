using AutoMapper;
using DGE.Core.Features.Minerals.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.Minerals.Commands.Handler
{
    public class MineralCommandHandler : ResponseHandler, IRequestHandler<AddMineralCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateMineralCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteMineralCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateMineralCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IMineralService _mineralService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public MineralCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IMineralService mineralService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _mineralService = mineralService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Mineral = _mapper.Map<Mineral>(request);
            if (request.MineralUrlFile != null)
                Mineral.MineralUrl = await _fileService.UploadFileAsync("Minerals", request.MineralUrlFile, FileCategoryEnum.Image);
            var result = await _mineralService.AddAsync(Mineral);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Mineral.Id });
        }

        public async Task<Response<string>> Handle(UpdateMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Mineral = await _mineralService.GetByIdAsync(request.Id);
            if (Mineral == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var MineralMapper = _mapper.Map(request, Mineral);
            if (request.MineralUrlFile != null)
                MineralMapper.MineralUrl = await _fileService.UploadFileAsync("Minerals", request.MineralUrlFile, FileCategoryEnum.Image);
            var result = await _mineralService.EditAsync(MineralMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Mineral = await _mineralService.GetByIdAsync(request.Id);
            if (Mineral == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _mineralService.HardDeleteAsync(Mineral);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Mineral = await _mineralService.GetByIdAsync(request.Id);
            if (Mineral == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Mineral.IsDeleted = !(Mineral.IsDeleted);
            Mineral.DeletionDate = DateTime.UtcNow;
            Mineral.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _mineralService.EditAsync(Mineral);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Mineral.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
