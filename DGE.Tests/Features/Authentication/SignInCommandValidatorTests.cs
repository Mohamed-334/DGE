using DGE.Core.Features.Authentication.Commands.RequestModels;
using DGE.Core.Features.Authentication.Commands.Validators;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Tests.TestDoubles;
using Moq;

namespace DGE.Tests.Features.Authentication
{
    public class SignInCommandValidatorTests
    {
        private readonly SignInCommandValidator _validator = new(new KeyEchoLocalizer(), new Mock<IUserService>().Object);

        [Fact]
        public async Task ValidRequest_Passes()
        {
            var result = await _validator.ValidateAsync(new SignInCommandRequestModel { Email = "user@test.com", Password = "P@ss123" });

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null, "P@ss123", "Email")]
        [InlineData("", "P@ss123", "Email")]
        [InlineData("user@test.com", null, "Password")]
        [InlineData("user@test.com", "", "Password")]
        public async Task MissingField_Fails(string? email, string? password, string invalidProperty)
        {
            var result = await _validator.ValidateAsync(new SignInCommandRequestModel { Email = email, Password = password });

            Assert.False(result.IsValid);
            Assert.All(result.Errors, error => Assert.Equal(invalidProperty, error.PropertyName));
            Assert.Contains(result.Errors, error => error.ErrorMessage == AppLocalizationKeys.NotEmpty);
        }
    }
}
