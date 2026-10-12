using AutoMapper;
using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Products.Commands.Handler
{
    public class ProductCommandHandler : ResponseHandler, IRequestHandler<AddProductCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductService productService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productService = productService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Product = _mapper.Map<Product>(request);
            var result = await _productService.AddAsync(Product);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Product.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Product = await _productService.GetByIdAsync(request.Id);
            if (Product == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductMapper = _mapper.Map(request, Product);
            var result = await _productService.EditAsync(ProductMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Product = await _productService.GetByIdAsync(request.Id);
            if (Product == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productService.HardDeleteAsync(Product);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Product = await _productService.GetByIdAsync(request.Id);
            if (Product == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Product.IsDeleted = !(Product.IsDeleted);
            Product.DeletionDate = DateTime.UtcNow;
            Product.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productService.EditAsync(Product);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Product.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
