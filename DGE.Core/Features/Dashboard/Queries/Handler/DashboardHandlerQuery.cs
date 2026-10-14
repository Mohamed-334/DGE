using DGE.Core.Features.Dashboard.Dto;
using DGE.Core.Features.Dashboard.Queries.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DGE.Core.Features.Dashboard.Queries.Handler
{
    public class DashboardHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetDashboardStatsQueryRequestModel, Response<DashboardStatsDto>>
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ICertificationService _certificationService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IContactUsService _contactUsService;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        #endregion

        #region Constructor
        public DashboardHandlerQuery(IProductService productService,
                                     IProductCategoryService productCategoryService,
                                     ICertificationService certificationService,
                                     IExhibitionService exhibitionService,
                                     IContactUsService contactUsService,
                                     IStringLocalizer<AppLocalization> stringLocalizer) : base(stringLocalizer)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _certificationService = certificationService;
            _exhibitionService = exhibitionService;
            _contactUsService = contactUsService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Methods
        // Counts skip soft-deleted rows. They run one after another because the services share one DbContext
        public async Task<Response<DashboardStatsDto>> Handle(GetDashboardStatsQueryRequestModel request, CancellationToken cancellationToken)
        {
            var Stats = new DashboardStatsDto
            {
                Products = await _productService.CountAsync(cancellationToken: cancellationToken),
                Categories = await _productCategoryService.CountAsync(cancellationToken: cancellationToken),
                Certifications = await _certificationService.CountAsync(cancellationToken: cancellationToken),
                Exhibitions = await _exhibitionService.CountAsync(cancellationToken: cancellationToken),
                Messages = await _contactUsService.CountAsync(cancellationToken: cancellationToken),
                UnreadMessages = await _contactUsService.CountAsync(c => !c.IsRead, cancellationToken)
            };
            return Success(Stats, _stringLocalizer[AppLocalizationKeys.Success]);
        }
        #endregion
    }
}
