using System;
using System.Windows.Forms;
using F28X_Toolset.Display;
using F28X_Toolset.Interfaces;
using F28X_Toolset.ResponseDTO;

namespace F28X_Toolset
{
    public partial class Form1 : Form
    {
        private readonly ISettings _settings;
        private readonly ComPortManager _comPortManager;
        private readonly LanguageManager _languageManager;
        private readonly Communication _communication;
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

            _comPortManager.ConnectionStateChanged += (_, connected) =>
               connectButtonToolStripMenuItem.BackColor = connected
                   ? Color.LightGreen
                   : SystemColors.Control;

            _comPortManager.OpenFailed += ComPortManager_OpenFailed;

            _languageManager = new LanguageManager(
                languagesToolStripComboBox,
                _settings.Language);

            _communication = new Communication(_comPortManager);

            _communication.DeviceConnected += Communication_DeviceConnected;
            _communication.DeviceDisconnected += Communication_DeviceDisconnected;
            _communication.DeviceNotFound += Communication_DeviceNotFound;

            _qmPoller = new QmPoller(
                _communication,
                readingValueLabel,
                unitLabel,
                stateLabel,
                formsPlot1,
                _settings.GraphMode);

            _comPortManager.Initialize();

            _languageManager.LanguageChanged += LanguageManager_LanguageChanged;
            _languageManager.LanguageCodeChanged += code => _settings.UpdateLanguage(code);
            _languageManager.Initialize();

            ApplyGraphMode(_settings.GraphMode);

            scrollingToolStripMenuItem.Click += GraphModeMenuItem_Click;
            continuousToolStripMenuItem.Click += GraphModeMenuItem_Click;
            basicToolStripMenuItem.CheckedChanged += BasicToolStripMenuItem_CheckedChanged;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _comPortManager.Dispose();
        }

        // ── ComPortManager eventy ────────────────────────────────────────────

        private void ComPortManager_OpenFailed(object? sender, OpenFailedArgs args)
        {
            var title = _localization?.Get("Error.PortTitle") ?? "Chyba portu";

            // Enum místo magic string — kompilátor hlídá všechny větve (#1)
            var message = args.Error switch
            {
                PortOpenError.Timeout =>
                    string.Format(
                        _localization?.Get("Error.PortTimeout") ?? "Port {0} neodpovídá (timeout).",
                        args.Detail),
                PortOpenError.OpenFailed =>
                    string.Format(
                        _localization?.Get("Error.PortOpen") ?? "Nelze otevřít port:\n{0}",
                        args.Detail),
                _ => args.Detail
            };

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ── Communication eventy ─────────────────────────────────────────────

        private void Communication_DeviceConnected(object? sender, IdResult result)
        {
            Invoke(() =>
            {
                modelToolStripStatusLabel.Text = result.Model;
                serialNumberToolStripStatusLabel.Text = result.Serial;
                versionToolStripStatusLabel.Text = result.Version;

                if (basicToolStripMenuItem.Checked)
                    _qmPoller.Start();
            });
        }

        private void Communication_DeviceDisconnected(object? sender, EventArgs e)
        {
            if (InvokeRequired) { Invoke(() => Communication_DeviceDisconnected(sender, e)); return; }

            modelToolStripStatusLabel.Text = string.Empty;
            serialNumberToolStripStatusLabel.Text = string.Empty;
            versionToolStripStatusLabel.Text = string.Empty;

            _qmPoller.Stop();
        }

        private void Communication_DeviceNotFound(object? sender, EventArgs e)
        {
            Invoke(() => MessageBox.Show(
                _localization?.Get("Dialog.DeviceNotFound") ?? "Zařízení Fluke nebylo nalezeno.",
                _localization?.Get("Dialog.DeviceNotFoundTitle") ?? "Neplatné zařízení",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning));
        }

        // ── Graf ─────────────────────────────────────────────────────────────

        private void GraphModeMenuItem_Click(object? sender, EventArgs e)
        {
            var mode = sender == scrollingToolStripMenuItem
                ? GraphMode.Scrolling
                : GraphMode.Continuous;

            ApplyGraphMode(mode);
            _settings.UpdateGraphMode(mode);
        }

        private void ApplyGraphMode(GraphMode mode)
        {
            scrollingToolStripMenuItem.Checked = mode == GraphMode.Scrolling;
            continuousToolStripMenuItem.Checked = mode == GraphMode.Continuous;
            _qmPoller.SetGraphMode(mode);
        }

        // ── Lokalizace ───────────────────────────────────────────────────────

        private void LanguageManager_LanguageChanged(ILocalizationProvider provider)
        {
            if (InvokeRequired) { Invoke(() => LanguageManager_LanguageChanged(provider)); return; }

            _localization = provider;
            _qmPoller.ApplyLocalization(provider);

            fileToolStripMenuItem.Text = provider.Get("Menu.File");
            exitToolStripMenuItem.Text = provider.Get("Menu.Exit");
            settingsToolStripMenuItem.Text = provider.Get("Menu.Settings");
            portToolStripMenuItem.Text = provider.Get("Menu.Port");
            languageToolStripMenuItem.Text = provider.Get("Menu.Language");
            displaedVluesToolStripMenuItem.Text = provider.Get("Menu.DisplayedValues");
            basicToolStripMenuItem.Text = provider.Get("Menu.Basic");
            advancedToolStripMenuItem.Text = provider.Get("Menu.Advanced");
            graphToolStripMenuItem.Text = provider.Get("Menu.Graph");
            scrollingToolStripMenuItem.Text = provider.Get("Menu.Graph.Scrolling");
            continuousToolStripMenuItem.Text = provider.Get("Menu.Graph.Continuous");
        }

        // ── Ostatní ──────────────────────────────────────────────────────────

        private void BasicToolStripMenuItem_CheckedChanged(object? sender, EventArgs e)
        {
            if (basicToolStripMenuItem.Checked)
                _qmPoller.Start();
            else
                _qmPoller.Stop();
        }

        private void ExitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                _localization?.Get("Dialog.ExitConfirm") ?? "Opravdu chcete ukončit?",
                _localization?.Get("Dialog.ExitTitle") ?? "Ukončit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Application.Exit();
        }
    }
}