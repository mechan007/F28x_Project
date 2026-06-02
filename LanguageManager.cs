using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using F28X_Toolset.Interfaces;
using F28X_Toolset.Localization;

namespace F28X_Toolset
{
    internal sealed class LanguageManager
    {
        private readonly ToolStripComboBox _languagesComboBox;
        private readonly string? _savedLanguageCode;

        // JSON každého souboru načten právě jednou — fix #9
        private readonly Dictionary<string, Dictionary<string, string>> _cache = new();

        public event Action<ILocalizationProvider>? LanguageChanged;
        public event Action<string>? LanguageCodeChanged;

        public LanguageManager(ToolStripComboBox languagesComboBox, string? savedLanguageCode)
        {
            _languagesComboBox = languagesComboBox;
            _savedLanguageCode = savedLanguageCode;
            _languagesComboBox.SelectedIndexChanged += LanguagesComboBox_SelectedIndexChanged;
        }

        public void Initialize()
        {
            LoadAvailableLanguages();
            var code = _savedLanguageCode ?? ResolveDefaultLanguage();
            ApplyLanguage(code);
        }

        private void LoadAvailableLanguages()
        {
            var langDir = Path.Combine(AppContext.BaseDirectory, "Lang");
            if (!Directory.Exists(langDir))
                return;

            _languagesComboBox.Items.Clear();
            _cache.Clear();

            foreach (var file in Directory.EnumerateFiles(langDir, "*.json").OrderBy(f => f))
            {
                var code = Path.GetFileNameWithoutExtension(file);

                try
                {
                    var json = File.ReadAllText(file);
                    var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                               ?? new Dictionary<string, string>();

                    _cache[code] = dict;   // uložíme do cache — ApplyLanguage již nečte disk

                    var displayName = dict.GetValueOrDefault("Language.Name") ?? code;
                    _languagesComboBox.Items.Add(new LanguageItem(code, displayName));
                }
                catch { }
            }
        }

        private void LanguagesComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_languagesComboBox.SelectedItem is not LanguageItem item)
                return;

            LanguageCodeChanged?.Invoke(item.Code);
            ApplyLanguage(item.Code);
        }

        private string ResolveDefaultLanguage()
        {
            var osCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return _cache.ContainsKey(osCode) ? osCode : "en";
        }

        private void ApplyLanguage(string code)
        {
            // fallback na "en" pokud kód není v cache
            if (!_cache.TryGetValue(code, out var dict))
            {
                if (!_cache.TryGetValue("en", out dict))
                    return;
                code = "en";
            }

            _languagesComboBox.SelectedIndexChanged -= LanguagesComboBox_SelectedIndexChanged;
            foreach (var item in _languagesComboBox.Items.OfType<LanguageItem>())
            {
                if (item.Code == code)
                {
                    _languagesComboBox.SelectedItem = item;
                    break;
                }
            }
            _languagesComboBox.SelectedIndexChanged += LanguagesComboBox_SelectedIndexChanged;

            LanguageChanged?.Invoke(new LocalizationProvider(dict));
        }

        private sealed class LanguageItem(string code, string displayName)
        {
            public string Code { get; } = code;
            public string DisplayName { get; } = displayName;
            public override string ToString() => DisplayName;
        }
    }
}