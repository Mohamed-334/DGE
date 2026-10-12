using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Service.Service
{
    public class FileService : IFileService
    {
        #region Fields
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly ILogger<FileService> _logger;

        private const long MaxImageSizeBytes = 5 * 1024 * 1024;     // 5 MB
        private const long MaxDocumentSizeBytes = 10 * 1024 * 1024; // 10 MB

        // Signatures (magic numbers) of the file formats we accept
        private static readonly byte[] JpegSignature = [0xFF, 0xD8, 0xFF];
        private static readonly byte[] PngSignature = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
        private static readonly byte[] GifSignature = [0x47, 0x49, 0x46, 0x38];
        private static readonly byte[] RiffSignature = [0x52, 0x49, 0x46, 0x46];
        private static readonly byte[] PdfSignature = [0x25, 0x50, 0x44, 0x46];
        private static readonly byte[] ZipSignature = [0x50, 0x4B, 0x03, 0x04];                          // docx, xlsx, pptx
        private static readonly byte[] OleSignature = [0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1];  // doc, xls, ppt

        private static readonly Dictionary<string, AllowedFileType> AllowedFileTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            // Images
            [".jpg"] = new(["image/jpeg"], FileCategoryEnum.Image, MaxImageSizeBytes, [JpegSignature]),
            [".jpeg"] = new(["image/jpeg"], FileCategoryEnum.Image, MaxImageSizeBytes, [JpegSignature]),
            [".png"] = new(["image/png"], FileCategoryEnum.Image, MaxImageSizeBytes, [PngSignature]),
            [".gif"] = new(["image/gif"], FileCategoryEnum.Image, MaxImageSizeBytes, [GifSignature]),
            [".webp"] = new(["image/webp"], FileCategoryEnum.Image, MaxImageSizeBytes, [RiffSignature]),

            // Documents
            [".pdf"] = new(["application/pdf"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [PdfSignature]),
            [".doc"] = new(["application/msword"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [OleSignature]),
            [".docx"] = new(["application/vnd.openxmlformats-officedocument.wordprocessingml.document"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [ZipSignature]),
            [".xls"] = new(["application/vnd.ms-excel"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [OleSignature]),
            [".xlsx"] = new(["application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [ZipSignature]),
            [".ppt"] = new(["application/vnd.ms-powerpoint"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [OleSignature]),
            [".pptx"] = new(["application/vnd.openxmlformats-officedocument.presentationml.presentation"], FileCategoryEnum.Document, MaxDocumentSizeBytes, [ZipSignature]),

            // Plain text formats have no signature
            [".csv"] = new(["text/csv", "application/csv", "text/plain", "application/vnd.ms-excel"], FileCategoryEnum.Document, MaxDocumentSizeBytes, []),
            [".txt"] = new(["text/plain"], FileCategoryEnum.Document, MaxDocumentSizeBytes, []),
        };
        #endregion

        #region Constructor
        public FileService(IWebHostEnvironment webHostEnvironment, IStringLocalizer<AppLocalization> stringLocalizer, ILogger<FileService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
        }

        #endregion
        #region Methods

        public async Task<string> UploadFileAsync(string DirectorName, IFormFile file, FileCategoryEnum allowedCategories)
        {
            if (file == null || file.Length == 0)
                return _stringLocalizer[AppLocalizationKeys.NoImage];

            if (string.IsNullOrWhiteSpace(DirectorName) || DirectorName != Path.GetFileName(DirectorName))
                throw new ArgumentException("Invalid directory name.", nameof(DirectorName));

            var extension = Path.GetExtension(file.FileName);
            await ValidateFileAsync(file, extension, allowedCategories);

            // Never trust the user-supplied file name
            var safeFileName = $"{Guid.NewGuid()}{extension.ToLowerInvariant()}";
            var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", DirectorName);
            try
            {
                Directory.CreateDirectory(directoryPath);
                await using var fileStream = new FileStream(Path.Combine(directoryPath, safeFileName), FileMode.CreateNew);
                await file.CopyToAsync(fileStream);
                return $"/Images/{DirectorName}/{safeFileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save uploaded file to {Directory}", DirectorName);
                return _stringLocalizer[AppLocalizationKeys.FailedToUploadImage];
            }
        }

        private async Task ValidateFileAsync(IFormFile file, string extension, FileCategoryEnum allowedCategories)
        {
            var invalidTypeKey = allowedCategories == FileCategoryEnum.Image ? AppLocalizationKeys.InvalidImageType : AppLocalizationKeys.InvalidFileType;
            if (!AllowedFileTypes.TryGetValue(extension, out var allowedType) || !allowedCategories.HasFlag(allowedType.Category))
                throw FileValidationException(file, invalidTypeKey);

            if (file.Length > allowedType.MaxSizeBytes)
                throw FileValidationException(file, AppLocalizationKeys.InvalidFileSize);

            if (!allowedType.MimeTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase))
                throw FileValidationException(file, invalidTypeKey);

            if (allowedType.Signatures.Length > 0 && !await HasValidSignatureAsync(file, allowedType.Signatures))
                throw FileValidationException(file, AppLocalizationKeys.InvalidFileContent);
        }

        private static async Task<bool> HasValidSignatureAsync(IFormFile file, byte[][] signatures)
        {
            var header = new byte[signatures.Max(s => s.Length)];
            await using var stream = file.OpenReadStream();
            var bytesRead = await stream.ReadAtLeastAsync(header, header.Length, throwOnEndOfStream: false);
            return signatures.Any(signature => bytesRead >= signature.Length && header.AsSpan(0, signature.Length).SequenceEqual(signature));
        }

        private ValidationException FileValidationException(IFormFile file, string localizationKey)
            => new([new ValidationFailure(file.Name, _stringLocalizer[localizationKey])]);

        private sealed record AllowedFileType(string[] MimeTypes, FileCategoryEnum Category, long MaxSizeBytes, byte[][] Signatures);
        #endregion


    }
}
