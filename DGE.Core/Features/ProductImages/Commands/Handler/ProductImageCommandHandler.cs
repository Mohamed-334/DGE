using AutoMapper;
using DGE.Core.Features.ProductImages.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.ProductImages.Commands.Handler
{
    public class ProductImageCommandHandler : ResponseHandler, IRequestHandler<AddProductImageCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateProductImageCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteProductImageCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateProductImageCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IProductImageService _productImageService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public ProductImageCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductImageService productImageService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productImageService = productImageService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductImageCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImage = _mapper.Map<ProductImage>(request);
            if (request.ImagePathFile != null)
                ProductImage.ImagePath = await _fileService.UploadFileAsync("ProductImages", request.ImagePathFile, FileCategoryEnum.Image);
            var result = await _productImageService.AddAsync(ProductImage);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = ProductImage.Id });
        }

        public async Task<Response<string>> Handle(UpdateProductImageCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImage = await _productImageService.GetByIdAsync(request.Id);
            if (ProductImage == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var ProductImageMapper = _mapper.Map(request, ProductImage);
            if (request.ImagePathFile != null)
                ProductImageMapper.ImagePath = await _fileService.UploadFileAsync("ProductImages", request.ImagePathFile, FileCategoryEnum.Image);
            var result = await _productImageService.EditAsync(ProductImageMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteProductImageCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImage = await _productImageService.GetByIdAsync(request.Id);
            if (ProductImage == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _productImageService.HardDeleteAsync(ProductImage);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateProductImageCommandRequestModel request, CancellationToken cancellationToken)
        {
            var ProductImage = await _productImageService.GetByIdAsync(request.Id);
            if (ProductImage == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            ProductImage.IsDeleted = !(ProductImage.IsDeleted);
            ProductImage.DeletionDate = DateTime.UtcNow;
            ProductImage.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _productImageService.EditAsync(ProductImage);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (ProductImage.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
