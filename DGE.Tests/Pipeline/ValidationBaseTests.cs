using DGE.Core.Shared.ValidationBase;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace DGE.Tests.Pipeline
{
    public class ValidationBaseTests
    {
        public record SampleRequest(string? Name) : IRequest<string>;

        private static RequestHandlerDelegate<string> Next(Action onCalled) => _ =>
        {
            onCalled();
            return Task.FromResult("handled");
        };

        [Fact]
        public async Task Handle_NoValidators_CallsNext()
        {
            var called = false;
            var behavior = new ValidationBase<SampleRequest, string>([]);

            var result = await behavior.Handle(new SampleRequest(null), Next(() => called = true), CancellationToken.None);

            Assert.True(called);
            Assert.Equal("handled", result);
        }

        [Fact]
        public async Task Handle_ValidRequest_CallsNext()
        {
            var called = false;
            var validator = new InlineValidator<SampleRequest>();
            validator.RuleFor(r => r.Name).NotEmpty();
            var behavior = new ValidationBase<SampleRequest, string>([validator]);

            await behavior.Handle(new SampleRequest("valid"), Next(() => called = true), CancellationToken.None);

            Assert.True(called);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ThrowsWithFailuresFromAllValidatorsAndSkipsNext()
        {
            var called = false;
            var first = new Mock<IValidator<SampleRequest>>();
            first.Setup(v => v.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult([new ValidationFailure("Name", "first")]));
            var second = new Mock<IValidator<SampleRequest>>();
            second.Setup(v => v.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult([new ValidationFailure("Name", "second")]));
            var behavior = new ValidationBase<SampleRequest, string>([first.Object, second.Object]);

            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(new SampleRequest(null), Next(() => called = true), CancellationToken.None));

            Assert.False(called);
            Assert.Equal(["first", "second"], exception.Errors.Select(e => e.ErrorMessage));
        }
    }
}
