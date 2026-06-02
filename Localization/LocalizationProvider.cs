using F28X_Toolset.Interfaces;
using System.Collections.Generic;

namespace F28X_Toolset.Localization
{
    internal sealed class LocalizationProvider : ILocalizationProvider
    {
        private readonly IReadOnlyDictionary<string, string> _translations;

        public LocalizationProvider(IReadOnlyDictionary<string, string> translations)
        {
            _translations = translations;
        }

        public string Get(string key) =>
            _translations.TryGetValue(key, out var value) ? value : key;
    }
}
