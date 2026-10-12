using AutoMapper;
using DGE.Core.Features.PackingTypes.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.PackingTypes.Commands.Handler
{
    public class PackingTypeCommandHandler : ResponseHandler, IRequestHandler<AddPackingTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdatePackingTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeletePackingTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivatePackingTypeCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IPackingTypeService _packingTypeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public PackingTypeCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IPackingTypeService packingTypeService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _packingTypeService = packingTypeService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddPackingTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var PackingType = _mapper.Map<PackingType>(request);
            var result = await _packingTypeService.AddAsync(PackingType);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = PackingType.Id });
        }

        public async Task<Response<string>> Handle(UpdatePackingTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var PackingType = await _packingTypeService.GetByIdAsync(request.Id);
            if (PackingType == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var PackingTypeMapper = _mapper.Map(request, PackingType);
            var result = await _packingTypeService.EditAsync(PackingTypeMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeletePackingTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var PackingType = await _packingTypeService.GetByIdAsync(request.Id);
            if (PackingType == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _packingTypeService.HardDeleteAsync(PackingType);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivatePackingTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var PackingType = await _packingTypeService.GetByIdAsync(request.Id);
            if (PackingType == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            PackingType.IsDeleted = !(PackingType.IsDeleted);
            PackingType.DeletionDate = DateTime.UtcNow;
            PackingType.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _packingTypeService.EditAsync(PackingType);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (PackingType.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
