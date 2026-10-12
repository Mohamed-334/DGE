using AutoMapper;
using DGE.Core.Features.Types.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using Type = DGE.Domain.Entities.Type;

namespace DGE.Core.Features.Types.Commands.Handler
{
    public class TypeCommandHandler : ResponseHandler, IRequestHandler<AddTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateTypeCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ITypeService _typeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public TypeCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  ITypeService typeService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _typeService = typeService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Type = _mapper.Map<Type>(request);
            var result = await _typeService.AddAsync(Type);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Type.Id });
        }

        public async Task<Response<string>> Handle(UpdateTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Type = await _typeService.GetByIdAsync(request.Id);
            if (Type == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var TypeMapper = _mapper.Map(request, Type);
            var result = await _typeService.EditAsync(TypeMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Type = await _typeService.GetByIdAsync(request.Id);
            if (Type == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _typeService.HardDeleteAsync(Type);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Type = await _typeService.GetByIdAsync(request.Id);
            if (Type == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Type.IsDeleted = !(Type.IsDeleted);
            Type.DeletionDate = DateTime.UtcNow;
            Type.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _typeService.EditAsync(Type);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Type.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
