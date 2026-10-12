using AutoMapper;
using DGE.Core.Features.ProductVarieties.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductVarieties.Commands.Handler
{
    public class ProductVarietyCommandHandler : ResponseHandler, IRequestHandler<AddProductVarietyCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductVarietyCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductVarietyCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductVarietyCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductVarietyService _productVarietyService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductVarietyCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductVarietyService productVarietyService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productVarietyService = productVarietyService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductVarietyCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVariety = _mapper.Map<ProductVariety>(request);
            var result = await _productVarietyService.AddAsync(ProductVariety);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductVariety.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductVarietyCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVariety = await _productVarietyService.GetByIdAsync(request.Id);
            if (ProductVariety == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductVarietyMapper = _mapper.Map(request, ProductVariety);
            var result = await _productVarietyService.EditAsync(ProductVarietyMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductVarietyCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVariety = await _productVarietyService.GetByIdAsync(request.Id);
            if (ProductVariety == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productVarietyService.HardDeleteAsync(ProductVariety);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductVarietyCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVariety = await _productVarietyService.GetByIdAsync(request.Id);
            if (ProductVariety == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductVariety.IsDeleted = !(ProductVariety.IsDeleted);
            ProductVariety.DeletionDate = DateTime.UtcNow;
            ProductVariety.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productVarietyService.EditAsync(ProductVariety);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductVariety.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
