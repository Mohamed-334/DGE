using AutoMapper;
using DGE.Core.Features.ProductTypes.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductTypes.Commands.Handler
{
    public class ProductTypeCommandHandler : ResponseHandler, IRequestHandler<AddProductTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductTypeCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductTypeCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductTypeService _productTypeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductTypeCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductTypeService productTypeService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productTypeService = productTypeService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductType = _mapper.Map<ProductType>(request);
            var result = await _productTypeService.AddAsync(ProductType);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductType.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductType = await _productTypeService.GetByIdAsync(request.Id);
            if (ProductType == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductTypeMapper = _mapper.Map(request, ProductType);
            var result = await _productTypeService.EditAsync(ProductTypeMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductType = await _productTypeService.GetByIdAsync(request.Id);
            if (ProductType == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productTypeService.HardDeleteAsync(ProductType);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductTypeCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductType = await _productTypeService.GetByIdAsync(request.Id);
            if (ProductType == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductType.IsDeleted = !(ProductType.IsDeleted);
            ProductType.DeletionDate = DateTime.UtcNow;
            ProductType.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productTypeService.EditAsync(ProductType);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductType.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
