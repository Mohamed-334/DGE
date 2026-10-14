using DGE.Service.Shared.ExtensionMethods;

namespace DGE.Tests.Features.Products
{
    public class SlugGeneratorTests
    {
        [Theory]
        [InlineData("Fresh Mango", "fresh-mango")]
        [InlineData("  Fresh   Mango!! ", "fresh-mango")]
        [InlineData("Jalapeño & Lime", "jalapeno-lime")]
        [InlineData("مانجو", "")]
        [InlineData(null, "")]
        public void Generate_ProducesLowercaseDashedSlugs(string? name, string expected)
        {
            Assert.Equal(expected, SlugGenerator.Generate(name));
        }

        [Fact]
        public void Generate_CutsLongNamesToTheMaxLength()
        {
            var slug = SlugGenerator.Generate(string.Join(" ", Enumerable.Repeat("mango", 100)));
            Assert.True(slug.Length <= SlugGenerator.MaxLength);
            Assert.True(SlugGenerator.IsValid(slug));
        }

        [Theory]
        [InlineData("fresh-mango-2", true)]
        [InlineData("Fresh-Mango", false)]
        [InlineData("fresh--mango", false)]
        [InlineData("-mango", false)]
        [InlineData("fresh mango", false)]
        [InlineData("", false)]
        public void IsValid_AcceptsOnlyCanonicalSlugs(string slug, bool expected)
        {
            Assert.Equal(expected, SlugGenerator.IsValid(slug));
        }
    }
}
