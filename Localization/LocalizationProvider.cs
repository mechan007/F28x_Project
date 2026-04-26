using F28x_Project.Interfaces;
using System.Collections.Generic;

namespace F28x_Project.Localization
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
