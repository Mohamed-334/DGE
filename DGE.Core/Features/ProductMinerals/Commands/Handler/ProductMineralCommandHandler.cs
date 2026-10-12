using AutoMapper;
using DGE.Core.Features.ProductMinerals.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductMinerals.Commands.Handler
{
    public class ProductMineralCommandHandler : ResponseHandler, IRequestHandler<AddProductMineralCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductMineralCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductMineralCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductMineralCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductMineralService _productMineralService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductMineralCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductMineralService productMineralService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productMineralService = productMineralService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMineral = _mapper.Map<ProductMineral>(request);
            var result = await _productMineralService.AddAsync(ProductMineral);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductMineral.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMineral = await _productMineralService.GetByIdAsync(request.Id);
            if (ProductMineral == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductMineralMapper = _mapper.Map(request, ProductMineral);
            var result = await _productMineralService.EditAsync(ProductMineralMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMineral = await _productMineralService.GetByIdAsync(request.Id);
            if (ProductMineral == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productMineralService.HardDeleteAsync(ProductMineral);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductMineralCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductMineral = await _productMineralService.GetByIdAsync(request.Id);
            if (ProductMineral == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductMineral.IsDeleted = !(ProductMineral.IsDeleted);
            ProductMineral.DeletionDate = DateTime.UtcNow;
            ProductMineral.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productMineralService.EditAsync(ProductMineral);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductMineral.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
