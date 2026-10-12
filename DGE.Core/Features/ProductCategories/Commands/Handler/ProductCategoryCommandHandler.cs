using AutoMapper;
using DGE.Core.Features.ProductCategories.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductCategories.Commands.Handler
{
    public class ProductCategoryCommandHandler : ResponseHandler, IRequestHandler<AddProductCategoryCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductCategoryCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductCategoryCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductCategoryCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductCategoryCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductCategoryService productCategoryService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productCategoryService = productCategoryService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductCategoryCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategory = _mapper.Map<ProductCategory>(request);
            var result = await _productCategoryService.AddAsync(ProductCategory);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductCategory.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductCategoryCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategory = await _productCategoryService.GetByIdAsync(request.Id);
            if (ProductCategory == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductCategoryMapper = _mapper.Map(request, ProductCategory);
            var result = await _productCategoryService.EditAsync(ProductCategoryMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductCategoryCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategory = await _productCategoryService.GetByIdAsync(request.Id);
            if (ProductCategory == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productCategoryService.HardDeleteAsync(ProductCategory);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductCategoryCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductCategory = await _productCategoryService.GetByIdAsync(request.Id);
            if (ProductCategory == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductCategory.IsDeleted = !(ProductCategory.IsDeleted);
            ProductCategory.DeletionDate = DateTime.UtcNow;
            ProductCategory.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productCategoryService.EditAsync(ProductCategory);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductCategory.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
