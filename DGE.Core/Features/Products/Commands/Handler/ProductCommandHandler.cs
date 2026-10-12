using AutoMapper;
using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Domain.Shared.BaseEntity.Implementations;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;
using Type = DGE.Domain.Entities.Type;

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
        private readonly IFileService _fileService;
        private readonly ITypeService _typeService;
        private readonly IPackingTypeService _packingTypeService;
        #endregion

        #region Constructor
        public ProductCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IProductService productService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService,
                                  ITypeService typeService,
                                  IPackingTypeService packingTypeService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _productService = productService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
            _typeService = typeService;
            _packingTypeService = packingTypeService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddProductCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Product = _mapper.Map<Product>(request);
            var userName = _authenticatedUserService.GetAuthenticatedUserName();

            // Upload the images first; the product and all its related rows are then saved together in one SaveChanges
            Product.ProductImages = new List<ProductImage>();
            foreach (var image in request.Images?.Where(i => i != null && i.Length > 0) ?? [])
            {
                var imagePath = await _fileService.UploadFileAsync("Products", image, FileCategoryEnum.Image);
                if (!imagePath.StartsWith("/Images/"))
                    return BadRequest<string>(imagePath);
                Product.ProductImages.Add(Audited(new ProductImage { ImagePath = imagePath }, userName));
            }

            Product.ProductVarieties = CleanNames(request.VarietyNames)
                .Select(name => Audited(new ProductVariety { Name = name }, userName))
                .ToList();

            var typeNames = CleanNames(request.TypeNames);
            var existingTypes = typeNames.Count > 0 ? await _typeService.GetByNamesAsync(typeNames) : [];
            Product.ProductTypes = typeNames
                .Select(name => existingTypes.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase)) is { } existing
                    ? Audited(new ProductType { TypeId = existing.Id }, userName)
                    : Audited(new ProductType { Type = Audited(new Type { Name = name }, userName) }, userName))
                .ToList();

            var packingTypeNames = CleanNames(request.PackingTypeNames);
            var existingPackingTypes = packingTypeNames.Count > 0 ? await _packingTypeService.GetByNamesAsync(packingTypeNames) : [];
            Product.ProductPackings = packingTypeNames
                .Select(name => existingPackingTypes.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)) is { } existing
                    ? Audited(new ProductPacking { PackingTypeId = existing.Id }, userName)
                    : Audited(new ProductPacking { PackingType = Audited(new PackingType { Name = name }, userName) }, userName))
                .ToList();

            Product.ProductVitamins = (request.VitaminIds ?? []).Distinct()
                .Select(id => Audited(new ProductVitamin { VitaminId = id }, userName))
                .ToList();

            Product.ProductMinerals = (request.MineralIds ?? []).Distinct()
                .Select(id => Audited(new ProductMineral { MineralId = id }, userName))
                .ToList();

            Product.ProductHarvestMonths = (request.HarvestMonthCodes ?? []).Distinct()
                .Select(code => Audited(new ProductHarvestMonth
                {
                    MonthStatusCode = code,
                    MonthStatus = ((Month)code).SplitEnumName(),
                    MonthStatusDisplayName = ((Month)code).GetDisplayName()
                }, userName))
                .ToList();

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

        private static List<string> CleanNames(List<string>? names)
        {
            return (names ?? [])
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static TEntity Audited<TEntity>(TEntity entity, string userName) where TEntity : BaseEntity
        {
            entity.CreatorName = userName;
            entity.CreationDate = DateTime.Now;
            return entity;
        }
        #endregion
    }
}
