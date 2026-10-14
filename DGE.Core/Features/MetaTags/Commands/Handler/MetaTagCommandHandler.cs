using AutoMapper;
using DGE.Core.Features.MetaTags.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.MetaTags.Commands.Handler
{
    public class MetaTagCommandHandler : ResponseHandler, IRequestHandler<UpdateMetaTagCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IMetaTagService _metaTagService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public MetaTagCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  IMetaTagService metaTagService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _metaTagService = metaTagService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        // Creates the page's tags on the first save, updates them afterwards
        public async Task<Response<string>> Handle(UpdateMetaTagCommandRequestModel request, CancellationToken cancellationToken)
        {
            var page = request.Page!.Trim().ToLowerInvariant();
            var Tag = await _metaTagService.GetByPageAsync(page, asTracking: true);
            var isNew = Tag == null;
            Tag ??= new MetaTag { Page = page };

            _mapper.Map(request, Tag);
            if (request.OgImageFile != null)
            {
                var imagePath = await _fileService.UploadFileAsync("MetaTags", request.OgImageFile, FileCategoryEnum.Image);
                if (!imagePath.StartsWith("/Images/"))
                    return BadRequest<string>(imagePath);
                Tag.OgImage = imagePath;
            }

            if (isNew)
            {
                var addResult = await _metaTagService.AddAsync(Tag);
                if (addResult == _stringLocalizer[AppLocalizationKeys.AddFailed])
                    return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            }
            else
            {
                var editResult = await _metaTagService.EditAsync(Tag);
                if (editResult == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                    return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            }
            return Success("", _stringLocalizer[AppLocalizationKeys.Updated], new { Tag.Id, Tag.Page });
        }
        #endregion
    }
}
