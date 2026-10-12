using AutoMapper;
using DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.ProductHarvestMonths.Commands.Handler
{
    public class ProductHarvestMonthCommandHandler : ResponseHandler, IRequestHandler<AddProductHarvestMonthCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductHarvestMonthCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductHarvestMonthCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductHarvestMonthCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductHarvestMonthService _productHarvestMonthService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        #endregion

        #region Constructor
        public ProductHarvestMonthCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductHarvestMonthService productHarvestMonthService,
                                  IAuthenticatedUserService authenticatedUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productHarvestMonthService = productHarvestMonthService;
            _authenticatedUserService = authenticatedUserService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductHarvestMonthCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonth = _mapper.Map<ProductHarvestMonth>(request);
            var result = await _productHarvestMonthService.AddAsync(ProductHarvestMonth);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductHarvestMonth.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductHarvestMonthCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonth = await _productHarvestMonthService.GetByIdAsync(request.Id);
            if (ProductHarvestMonth == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductHarvestMonthMapper = _mapper.Map(request, ProductHarvestMonth);
            var result = await _productHarvestMonthService.EditAsync(ProductHarvestMonthMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductHarvestMonthCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonth = await _productHarvestMonthService.GetByIdAsync(request.Id);
            if (ProductHarvestMonth == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productHarvestMonthService.HardDeleteAsync(ProductHarvestMonth);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductHarvestMonthCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductHarvestMonth = await _productHarvestMonthService.GetByIdAsync(request.Id);
            if (ProductHarvestMonth == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductHarvestMonth.IsDeleted = !(ProductHarvestMonth.IsDeleted);
            ProductHarvestMonth.DeletionDate = DateTime.UtcNow;
            ProductHarvestMonth.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productHarvestMonthService.EditAsync(ProductHarvestMonth);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductHarvestMonth.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
