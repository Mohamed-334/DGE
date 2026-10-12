using AutoMapper;
using DGE.Core.Features.Exhibitions.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.Exhibitions.Commands.Handler
{
    public class ExhibitionCommandHandler : ResponseHandler, IRequestHandler<AddExhibitionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateExhibitionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteExhibitionCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateExhibitionCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IExhibitionService _exhibitionService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public ExhibitionCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IExhibitionService exhibitionService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _exhibitionService = exhibitionService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddExhibitionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibition = _mapper.Map<Exhibition>(request);
            if (request.ExhibitionUrlFile != null)
                Exhibition.ExhibitionUrl = await _fileService.UploadFileAsync("Exhibitions", request.ExhibitionUrlFile, FileCategoryEnum.Image);
            var result = await _exhibitionService.AddAsync(Exhibition);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Exhibition.Id });
        }

        public async Task<Response<string>> Handle(UpdateExhibitionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibition = await _exhibitionService.GetByIdAsync(request.Id);
            if (Exhibition == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ExhibitionMapper = _mapper.Map(request, Exhibition);
            if (request.ExhibitionUrlFile != null)
                ExhibitionMapper.ExhibitionUrl = await _fileService.UploadFileAsync("Exhibitions", request.ExhibitionUrlFile, FileCategoryEnum.Image);
            var result = await _exhibitionService.EditAsync(ExhibitionMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteExhibitionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibition = await _exhibitionService.GetByIdAsync(request.Id);
            if (Exhibition == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _exhibitionService.HardDeleteAsync(Exhibition);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateExhibitionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Exhibition = await _exhibitionService.GetByIdAsync(request.Id);
            if (Exhibition == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Exhibition.IsDeleted = !(Exhibition.IsDeleted);
            Exhibition.DeletionDate = DateTime.UtcNow;
            Exhibition.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _exhibitionService.EditAsync(Exhibition);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Exhibition.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
