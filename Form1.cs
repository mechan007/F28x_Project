using System;
using System.Windows.Forms;
using F28x_Project.Display;
using F28x_Project.Interfaces;

namespace F28x_Project
{
    public partial class Form1 : Form
    {
        private readonly ISettings _settings;
        private readonly ComPortManager _comPortManager;
        private readonly LanguageManager _languageManager;
        private readonly Communication _comunication;
        private readonly QmPoller _qmPoller;
        private ILocalizationProvider? _localization;

        public Form1()
        {
            InitializeComponent();

            _settings = new SettingsFileManager();

            _comPortManager = new ComPortManager(
                portsToolStripComboBox,
                portToolStripText,
                connectButtonToolStripMenuItem,
                _settings);

            _languageManager = new LanguageManager(
                languagesToolStripComboBox,
                _settings);

            _comunication = new Communication(
                _comPortManager,
                modelToolStripStatusLabel,
                serialNumberToolStripStatusLabel,
                versionToolStripStatusLabel);

            _qmPoller = new QmPoller(
                _comunication,
                readingValueLabel,
                unitLabel,
                stateLabel);

            _comPortManager.Initialize();

            _languageManager.LanguageChanged += LanguageManager_LanguageChanged;
            _languageManager.Initialize();

            basicToolStripMenuItem.CheckedChanged += BasicToolStripMenuItem_CheckedChanged;

            if (basicToolStripMenuItem.Checked)
                _qmPoller.Start();


            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
        }

        private void ExitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                _localization?.Get("Dialog.ExitConfirm") ?? "Do you really want to exit?",
                _localization?.Get("Dialog.ExitTitle") ?? "Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Application.Exit();
        }

        private void LanguageManager_LanguageChanged(ILocalizationProvider provider)
        {
            _localization = provider;
            
            if (InvokeRequired) 
            {
                Invoke(() => LanguageManager_LanguageChanged(provider));
                return;
            }

            _qmPoller.ApplyLocalization(provider);

            fileToolStripMenuItem.Text = provider.Get("Menu.File");
            exitToolStripMenuItem.Text = provider.Get("Menu.Exit");
            settingsToolStripMenuItem.Text = provider.Get("Menu.Settings");
            portToolStripMenuItem.Text = provider.Get("Menu.Port");
            languageToolStripMenuItem.Text = provider.Get("Menu.Language");
            displaedVluesToolStripMenuItem.Text = provider.Get("Menu.DisplayedValues");
            basicToolStripMenuItem.Text = provider.Get("Menu.Basic");
            advancedToolStripMenuItem.Text = provider.Get("Menu.Advanced");
        }

        private void BasicToolStripMenuItem_CheckedChanged(object? sender, EventArgs e)
        {
            if (basicToolStripMenuItem.Checked)
                _qmPoller.Start();
            else
                _qmPoller.Stop();
        }
    }
}