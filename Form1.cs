using System;
using System.Windows.Forms;
using F28x_Project.Display;
using F28x_Project.Interfaces;

namespace F28x_Project
{
    public partial class Form1 : Form
    {
        private readonly SettingsFileManager _settingsFileManager;
        private readonly ComPortManager _comPortManager;
        private readonly LanguageManager _languageManager;
        private readonly Communication _comunication;
        private readonly QmPoller _qmPoller;


        public Form1()
        {
            InitializeComponent();

            _settingsFileManager = new SettingsFileManager();

            _comPortManager = new ComPortManager(
                portsToolStripComboBox,
                portToolStripText,
                connectButtonToolStripMenuItem,
                _settingsFileManager);

            _languageManager = new LanguageManager(
                languagesToolStripComboBox,
                _settingsFileManager);

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
            _languageManager.Initialize();

            basicToolStripMenuItem.CheckedChanged += BasicToolStripMenuItem_CheckedChanged;

            if (basicToolStripMenuItem.Checked)
                _qmPoller.Start();
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