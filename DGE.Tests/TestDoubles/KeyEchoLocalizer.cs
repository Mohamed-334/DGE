using DGE.Infrastructure.Shared.Localization;
using Microsoft.Extensions.Localization;

namespace DGE.Tests.TestDoubles
{
    public class KeyEchoLocalizer : IStringLocalizer<AppLocalization>
    {
        public LocalizedString this[string name] => new(name, name);

        public LocalizedString this[string name, params object[] arguments] => new(name, name);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => [];
    }
}
