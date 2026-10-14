using System.Text;
using System.Text.RegularExpressions;

namespace DGE.Service.Shared.ExtensionMethods
{
    public static partial class SlugGenerator
    {
        public const int MaxLength = 200;

        // Lowercase latin letters and digits separated by single dashes, e.g. "fresh-mango-2"
        [GeneratedRegex("^[a-z0-9]+(?:-[a-z0-9]+)*$")]
        private static partial Regex SlugPattern();

        [GeneratedRegex("[^a-z0-9]+")]
        private static partial Regex NonSlugCharacters();

        public static bool IsValid(string? slug) =>
            !string.IsNullOrEmpty(slug) && slug.Length <= MaxLength && SlugPattern().IsMatch(slug);

        // Returns an empty string when the text has no latin letters or digits (e.g. an Arabic-only name)
        public static string Generate(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Strip accents so "Jalapeño" becomes "jalapeno" instead of "jalape-o"
            var decomposed = text.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var withoutAccents = new StringBuilder(decomposed.Length);
            foreach (var c in decomposed)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    withoutAccents.Append(c);
            }

            var slug = NonSlugCharacters().Replace(withoutAccents.ToString(), "-").Trim('-');
            return slug.Length > MaxLength ? slug[..MaxLength].TrimEnd('-') : slug;
        }
    }
}
