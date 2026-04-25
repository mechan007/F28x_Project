using System;
using System.Collections.Generic;
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
            ApplyLanguage(_settings.Language ?? "en");
        }

        private void LoadAvailableLanguages()
        {
            var langDir = Path.Combine(AppContext.BaseDirectory, "Lang");
            if (!Directory.Exists(langDir))
                return;

            var languages = Directory.EnumerateFiles(langDir, "*.json")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .OrderBy(l => l)
                .ToArray();

            _languagesComboBox.Items.Clear();
            _languagesComboBox.Items.AddRange(languages.Cast<object>().ToArray());
        }

        private void LanguagesComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_languagesComboBox.SelectedItem is string language)
            {
                _settings.UpdateLanguage(language);
                ApplyLanguage(language);
            }
        }

        private void ApplyLanguage(string language)
        {
            var langDir = Path.Combine(AppContext.BaseDirectory, "Lang");
            var filePath = Path.Combine(langDir, $"{language}.json");

            if (!File.Exists(filePath))
                filePath = Path.Combine(langDir, "en.json");

            if (!File.Exists(filePath))
                return;

            try
            {
                var json = File.ReadAllText(filePath);
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                    ?? new Dictionary<string, string>();

                var provider = new LocalizationProvider(dict);
                LanguageChanged?.Invoke(provider);

                // vyber v comboboxu
                if (_languagesComboBox.Items.Contains(language))
                    _languagesComboBox.SelectedItem = language;
            }
            catch
            {
                // při chybě načtení zůstane původní jazyk
            }
        }
    }
}