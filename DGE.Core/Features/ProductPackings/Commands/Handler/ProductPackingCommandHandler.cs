using AutoMapper;
using DGE.Core.Features.ProductPackings.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductPackings.Commands.Handler
{
    public class ProductPackingCommandHandler : ResponseHandler, IRequestHandler<AddProductPackingCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductPackingCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductPackingCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductPackingCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductPackingService _productPackingService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductPackingCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductPackingService productPackingService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productPackingService = productPackingService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductPackingCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPacking = _mapper.Map<ProductPacking>(request);
            var result = await _productPackingService.AddAsync(ProductPacking);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductPacking.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductPackingCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPacking = await _productPackingService.GetByIdAsync(request.Id);
            if (ProductPacking == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductPackingMapper = _mapper.Map(request, ProductPacking);
            var result = await _productPackingService.EditAsync(ProductPackingMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductPackingCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPacking = await _productPackingService.GetByIdAsync(request.Id);
            if (ProductPacking == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productPackingService.HardDeleteAsync(ProductPacking);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductPackingCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductPacking = await _productPackingService.GetByIdAsync(request.Id);
            if (ProductPacking == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductPacking.IsDeleted = !(ProductPacking.IsDeleted);
            ProductPacking.DeletionDate = DateTime.UtcNow;
            ProductPacking.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productPackingService.EditAsync(ProductPacking);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductPacking.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
