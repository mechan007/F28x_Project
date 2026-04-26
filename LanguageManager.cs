using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using F28x_Project.Interfaces;
using F28x_Project.Localization;

namespace F28x_Project
{
    internal sealed class LanguageManager
    {
        private readonly ToolStripComboBox _languagesComboBox;
        private readonly ISettings _settings;

        public event Action<ILocalizationProvider>? LanguageChanged;

        public LanguageManager(ToolStripComboBox languagesComboBox, ISettings settings)
        {
            _languagesComboBox = languagesComboBox;
            _settings = settings;

            _languagesComboBox.SelectedIndexChanged += LanguagesComboBox_SelectedIndexChanged;
        }

        public void Initialize()
        {
            LoadAvailableLanguages();

            var code = _settings.Language ?? ResolveDefaultLanguage();

            if (_settings.Language is null)
                _settings.UpdateLanguage(code);

            ApplyLanguage(code);
        }

        private void LoadAvailableLanguages()
        {
            var langDir = Path.Combine(AppContext.BaseDirectory, "Lang");
            if (!Directory.Exists(langDir))
                return;

            if (!Directory.Exists(langDir))
                return;

            _languagesComboBox.Items.Clear();

            foreach (var file in Directory.EnumerateFiles(langDir, "*.json").OrderBy(f => f))
            {
                var code = Path.GetFileNameWithoutExtension(file);
                string displayName = code;

                try
                {
                    var json = File.ReadAllText(file);
                    var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    displayName = dict?.GetValueOrDefault("Language.Name") ?? code;
                }
                catch { }
                // každá položka nese kód jako Tag
                _languagesComboBox.Items.Add(new LanguageItem(code, displayName));
            }
        }

        private void LanguagesComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_languagesComboBox.SelectedItem is not LanguageItem item)
                return;

            _settings.UpdateLanguage(item.Code);  // uloží "cs", "en"... do settings.json
            ApplyLanguage(item.Code);
        }

        private string ResolveDefaultLanguage()
        {
            var langDir = Path.Combine(AppContext.BaseDirectory, "Lang");
            var osCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (File.Exists(Path.Combine(langDir, $"{osCode}.json")))
                return osCode;

            return "en";
        }

        private void ApplyLanguage(string code)
        {
            var langDir = Path.Combine(AppContext.BaseDirectory, "Lang");
            var filePath = Path.Combine(langDir, $"{code}.json");

            if (!File.Exists(filePath))
            {
                code = "en";
                filePath = Path.Combine(langDir, "en.json");
            }

            if (!File.Exists(filePath))
                return;

            try
            {
                var json = File.ReadAllText(filePath);
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                    ?? new Dictionary<string, string>();

                // 1. nastav combobox bez spuštění eventu
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

                // 2. přelož UI
                LanguageChanged?.Invoke(new LocalizationProvider(dict));
            }
            catch { }
        }

        // pomocná třída — combobox zobrazí DisplayName, kód máme vždy k dispozici
        private sealed class LanguageItem
        {
            public string Code { get; }
            public string DisplayName { get; }

            public LanguageItem(string code, string displayName)
            {
                Code = code;
                DisplayName = displayName;
            }

            public override string ToString() => DisplayName;
        }
    }
}