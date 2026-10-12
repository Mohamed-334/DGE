using AutoMapper;
using DGE.Core.Features.Certifications.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Core.Features.Certifications.Commands.Handler
{
    public class CertificationCommandHandler : ResponseHandler, IRequestHandler<AddCertificationCommandRequestModel, Response<string>>,
                                                         IRequestHandler<UpdateCertificationCommandRequestModel, Response<string>>,
                                                         IRequestHandler<DeleteCertificationCommandRequestModel, Response<string>>,
                                                         IRequestHandler<SoftDeleteAndActivateCertificationCommandRequestModel, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ICertificationService _certificationService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public CertificationCommandHandler(IStringLocalizer<AppLocalization> stringLocalizer,
                                  IMapper mapper,
                                  ICertificationService certificationService,
                                  IAuthenticatedUserService authenticatedUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _certificationService = certificationService;
            _authenticatedUserService = authenticatedUserService;
            _fileService = fileService;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddCertificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Certification = _mapper.Map<Certification>(request);
            if (request.CertificationUrlFile != null)
                Certification.CertificationUrl = await _fileService.UploadFileAsync("Certifications", request.CertificationUrlFile, FileCategoryEnum.All);
            var result = await _certificationService.AddAsync(Certification);
            if (result == _stringLocalizer[AppLocalizationKeys.AddFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.AddFailed]);
            return Success("", Meta: new { Id = Certification.Id });
        }

        public async Task<Response<string>> Handle(UpdateCertificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Certification = await _certificationService.GetByIdAsync(request.Id);
            if (Certification == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);

            var CertificationMapper = _mapper.Map(request, Certification);
            if (request.CertificationUrlFile != null)
                CertificationMapper.CertificationUrl = await _fileService.UploadFileAsync("Certifications", request.CertificationUrlFile, FileCategoryEnum.All);
            var result = await _certificationService.EditAsync(CertificationMapper);
            if (result == _stringLocalizer[AppLocalizationKeys.UpdateFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.UpdateFailed]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteCertificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Certification = await _certificationService.GetByIdAsync(request.Id);
            if (Certification == null)
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            var result = await _certificationService.HardDeleteAsync(Certification);
            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(SoftDeleteAndActivateCertificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var Certification = await _certificationService.GetByIdAsync(request.Id);
            if (Certification == null)
                return NotFound<string>(_stringLocalizer[AppLocalizationKeys.NotFound]);
            Certification.IsDeleted = !(Certification.IsDeleted);
            Certification.DeletionDate = DateTime.UtcNow;
            Certification.DeleterName = _authenticatedUserService.GetAuthenticatedUserName();
            var result = await _certificationService.EditAsync(Certification);

            if (result == _stringLocalizer[AppLocalizationKeys.DeletedFailed])
                return BadRequest<string>(_stringLocalizer[AppLocalizationKeys.DeletedFailed]);
            if (Certification.IsDeleted)
                return Deleted<string>(_stringLocalizer[AppLocalizationKeys.Deleted]);
            return Success<string>(msg: _stringLocalizer[AppLocalizationKeys.Activated]);
        }
        #endregion
    }
}
