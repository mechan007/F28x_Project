using System.Linq;
using System.Windows.Forms;
using F28x_Project.Interfaces;

namespace F28x_Project
{
    internal sealed class LanguageManager
    {
        private readonly ToolStripComboBox _languagesComboBox;
        private readonly ISettings _settings;

        public LanguageManager(ToolStripComboBox languagesComboBox, ISettings settings)
        {
            _languagesComboBox = languagesComboBox;
            _settings = settings;

            _languagesComboBox.SelectedIndexChanged += LanguagesComboBox_SelectedIndexChanged;
        }

        public void Initialize()
        {
            ApplyLanguage(_settings.Language);
        }

        private void LanguagesComboBox_SelectedIndexChanged(object? sender, System.EventArgs e)
        {
            if (_languagesComboBox.SelectedItem is string language)
            {
                _settings.UpdateLanguage(language);
            }
        }

        private void ApplyLanguage(string? preferredLanguage)
        {
            if (string.IsNullOrWhiteSpace(preferredLanguage))
            {
                return;
            }

            var items = _languagesComboBox.Items.Cast<object>().ToList();
            if (items.Contains(preferredLanguage))
            {
                _languagesComboBox.SelectedItem = preferredLanguage;
            }
        }
    }
}