using AutoMapper;
using DGE.Core.Features.ProductVitamins.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductVitamins.Commands.Handler
{
    public class ProductVitaminCommandHandler : ResponseHandler, IRequestHandler<AddProductVitaminCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductVitaminCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductVitaminCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductVitaminCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductVitaminService _productVitaminService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductVitaminCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductVitaminService productVitaminService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productVitaminService = productVitaminService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamin = _mapper.Map<ProductVitamin>(request);
            var result = await _productVitaminService.AddAsync(ProductVitamin);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductVitamin.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamin = await _productVitaminService.GetByIdAsync(request.Id);
            if (ProductVitamin == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductVitaminMapper = _mapper.Map(request, ProductVitamin);
            var result = await _productVitaminService.EditAsync(ProductVitaminMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamin = await _productVitaminService.GetByIdAsync(request.Id);
            if (ProductVitamin == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productVitaminService.HardDeleteAsync(ProductVitamin);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductVitaminCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductVitamin = await _productVitaminService.GetByIdAsync(request.Id);
            if (ProductVitamin == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductVitamin.IsDeleted = !(ProductVitamin.IsDeleted);
            ProductVitamin.DeletionDate = DateTime.UtcNow;
            ProductVitamin.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productVitaminService.EditAsync(ProductVitamin);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductVitamin.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
