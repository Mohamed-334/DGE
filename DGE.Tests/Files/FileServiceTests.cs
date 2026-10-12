using DGE.Infrastructure.Shared.Localization;
using DGE.Service.Service;
using DGE.Tests.TestDoubles;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Tests.Files
{
    public class FileServiceTests : IDisposable
    {
        private static readonly byte[] PngBytes = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00];
        private static readonly byte[] PdfBytes = [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x37];

        private readonly string _webRoot = Path.Combine(Path.GetTempPath(), $"file-service-tests-{Guid.NewGuid():N}");
        private readonly FileService _fileService;

        public FileServiceTests()
        {
            var environment = new Mock<IWebHostEnvironment>();
            environment.SetupGet(e => e.WebRootPath).Returns(_webRoot);
            _fileService = new FileService(environment.Object, new KeyEchoLocalizer(), NullLogger<FileService>.Instance);
        }

        private static IFormFile CreateFile(string fileName, string contentType, byte[] content)
            => new FormFile(new MemoryStream(content), 0, content.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

        private static string SingleError(ValidationException exception) => Assert.Single(exception.Errors).ErrorMessage;

        [Fact]
        public async Task ValidImage_IsSavedUnderGuidName()
        {
            var path = await _fileService.UploadFileAsync("Users", CreateFile("../../evil name.PNG", "image/png", PngBytes), FileCategoryEnum.Image);

            Assert.StartsWith("/Images/Users/", path);
            Assert.EndsWith(".png", path);
            Assert.DoesNotContain("evil", path);
            Assert.True(File.Exists(Path.Combine(_webRoot, path.TrimStart('/').Replace('/', Path.DirectorySeparatorChar))));
        }

        [Fact]
        public async Task ValidDocument_IsAcceptedWhenDocumentsAllowed()
        {
            var path = await _fileService.UploadFileAsync("Docs", CreateFile("report.pdf", "application/pdf", PdfBytes), FileCategoryEnum.All);

            Assert.EndsWith(".pdf", path);
        }

        [Fact]
        public async Task Document_IsRejectedWhenOnlyImagesAllowed()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _fileService.UploadFileAsync("Users", CreateFile("report.pdf", "application/pdf", PdfBytes), FileCategoryEnum.Image));

            Assert.Equal(AppLocalizationKeys.InvalidImageType, SingleError(exception));
        }

        [Fact]
        public async Task UnknownExtension_IsRejected()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _fileService.UploadFileAsync("Docs", CreateFile("payload.exe", "application/octet-stream", [0x4D, 0x5A]), FileCategoryEnum.All));

            Assert.Equal(AppLocalizationKeys.InvalidFileType, SingleError(exception));
        }

        [Fact]
        public async Task MismatchedMimeType_IsRejected()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _fileService.UploadFileAsync("Users", CreateFile("photo.png", "text/html", PngBytes), FileCategoryEnum.Image));

            Assert.Equal(AppLocalizationKeys.InvalidImageType, SingleError(exception));
        }

        [Fact]
        public async Task SpoofedContent_IsRejected()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _fileService.UploadFileAsync("Users", CreateFile("photo.png", "image/png", "<script>"u8.ToArray()), FileCategoryEnum.Image));

            Assert.Equal(AppLocalizationKeys.InvalidFileContent, SingleError(exception));
        }

        [Fact]
        public async Task OversizedImage_IsRejected()
        {
            var content = new byte[5 * 1024 * 1024 + 1];
            PngBytes.CopyTo(content, 0);

            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _fileService.UploadFileAsync("Users", CreateFile("photo.png", "image/png", content), FileCategoryEnum.Image));

            Assert.Equal(AppLocalizationKeys.InvalidFileSize, SingleError(exception));
        }

        [Theory]
        [InlineData("../outside")]
        [InlineData("a/b")]
        [InlineData("")]
        public async Task InvalidDirectoryName_Throws(string directory)
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _fileService.UploadFileAsync(directory, CreateFile("photo.png", "image/png", PngBytes), FileCategoryEnum.Image));
        }

        [Fact]
        public async Task EmptyFile_ReturnsNoImageMessage()
        {
            var result = await _fileService.UploadFileAsync("Users", CreateFile("photo.png", "image/png", []), FileCategoryEnum.Image);

            Assert.Equal(AppLocalizationKeys.NoImage, result);
        }

        public void Dispose()
        {
            if (Directory.Exists(_webRoot))
                Directory.Delete(_webRoot, recursive: true);
        }
    }
}
