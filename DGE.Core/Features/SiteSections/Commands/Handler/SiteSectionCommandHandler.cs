using AutoMapper;
using DGE.Core.Features.SiteSections.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.SiteSections.Commands.Handler
{
    public class SiteSectionCommandHandler : ResponseHandler, IRequestHandler<UpdateSiteSectionCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ISiteSectionService _siteSectionService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public SiteSectionCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  ISiteSectionService siteSectionService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _siteSectionService = siteSectionService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        // Creates the section on its first save, updates it afterwards
        public async Task<Response<string>> Handle(UpdateSiteSectionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var key = request.Key!.Trim().ToLowerInvariant();
            var Section = await _siteSectionService.GetByKeyAsync(key, asTracking: true);
            var isNew = Section == null;
            Section ??= new SiteSection { Key = key, Items = new List<SiteSectionItem>() };

            _mapper.Map(request, Section);
            if (request.ImageFile != null)
            {
                var imagePath = await _fileService.UploadFileAsync("SiteSections", request.ImageFile, FileCategoryEnum.Image);
                if (!imagePath.StartsWith("/Images/"))
                    return BadRequest<string>(imagePath);
                Section.Image = imagePath;
            }

            if (request.ClearItems || request.Items is { Count: > 0 })
            {
                var userName = _authenticatedUserService.GetAuthenticatedUserName();
                Section.Items!.Clear();
                var order = 0;
                foreach (var item in request.Items ?? [])
                {
                    Section.Items.Add(new SiteSectionItem
                    {
                        Title = item.Title,
                        TitleLocalization = item.TitleLocalization,
                        Description = item.Description,
                        DescriptionLocalization = item.DescriptionLocalization,
                        Icon = item.Icon,
                        DisplayOrder = order++,
                        CreatorName = userName,
                        CreationDate = DateTime.Now
                    });
                }
            }

            if (isNew)
            {
                var addResult = await _siteSectionService.AddAsync(Section);
                if (addResult == _stringLocalizer[AppLocalizationKeys.AddFailed])
                    return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            }
            else
            {
                var editResult = await _siteSectionService.EditAsync(Section);
                if (editResult == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                    return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            }
            return Success("", _stringLocalizer[AppLocalizationKeys.Updated], new { Section.Id, Section.Key });
        }
        #endregion
    }
}
