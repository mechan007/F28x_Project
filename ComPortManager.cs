using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using F28x_Project.Interfaces;

namespace F28x_Project
{
    internal sealed class ComPortManager : IDisposable
    {
        private const int OpenTimeoutMs = 2000;

        private readonly ToolStripComboBox _portsComboBox;
        private readonly ToolStripTextBox _portTextBox;
        private readonly ToolStripMenuItem _connectMenuItem;
        private readonly ISettings _settings;
        private readonly List<string> _ports = new();
        private SerialPort? _serialPort;

        /// <summary>Fired on background thread — PortOpened handler smí blokovat.</summary>
        public event EventHandler? PortOpened;

        /// <summary>Fired on UI thread.</summary>
        public event EventHandler? PortClosed;

        /// <summary>Fired on UI thread. bool = true → připojeno, false → odpojeno.</summary>
        public event EventHandler<bool>? ConnectionStateChanged;

        /// <summary>Fired on UI thread když se port nepodaří otevřít.</summary>
        public event EventHandler<OpenFailedArgs>? OpenFailed;

        public ComPortManager(
            ToolStripComboBox portsComboBox,
            ToolStripTextBox portTextBox,
            ToolStripMenuItem connectMenuItem,
            ISettings settings)
        {
            _portsComboBox = portsComboBox;
            _portTextBox = portTextBox;
            _connectMenuItem = connectMenuItem;
            _settings = settings;

            _portsComboBox.DropDown += PortsComboBox_DropDown;
            _portsComboBox.SelectedIndexChanged += PortsComboBox_SelectedIndexChanged;
            _connectMenuItem.Click += ConnectMenuItem_Click;

            NotifyConnectionState(false);
        }

        public IReadOnlyList<string> Ports => _ports;

        public void Initialize()
        {
            RefreshPortsAndBind(_settings.Port);
        }

        private void PortsComboBox_DropDown(object? sender, EventArgs e)
        {
            RefreshPortsAndBind(_portsComboBox.SelectedItem as string);
        }

        private void PortsComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_portsComboBox.SelectedItem is string portName)
            {
                _portTextBox.Text = portName;
                _settings.UpdatePort(portName);
            }
        }

        private async void ConnectMenuItem_Click(object? sender, EventArgs e)
        {
            if (_serialPort?.IsOpen == true)
            {
                ClosePort();
                return;
            }

            var portName = GetSelectedPort();
            if (string.IsNullOrWhiteSpace(portName))
                return;

            _connectMenuItem.Enabled = false;
            await OpenPortAsync(portName);
            _connectMenuItem.Enabled = true;
        }

        private async Task OpenPortAsync(string portName)
        {
            ClosePort();

            var serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

            try
            {
                var openTask = Task.Run(() => serialPort.Open());
                var timeoutTask = Task.Delay(OpenTimeoutMs);

                if (await Task.WhenAny(openTask, timeoutTask) == timeoutTask)
                {
                    _ = Task.Run(() => { try { serialPort.Dispose(); } catch { } });
                    NotifyConnectionState(false);
                    OpenFailed?.Invoke(this, new OpenFailedArgs(PortOpenError.Timeout, portName));
                    return;
                }

                if (openTask.IsFaulted)
                {
                    serialPort.Dispose();
                    NotifyConnectionState(false);
                    var detail = openTask.Exception?.InnerException?.Message ?? portName;
                    OpenFailed?.Invoke(this, new OpenFailedArgs(PortOpenError.OpenFailed, detail));
                    return;
                }

                _serialPort = serialPort;
                NotifyConnectionState(true);

                await Task.Run(() => PortOpened?.Invoke(this, EventArgs.Empty));
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or InvalidOperationException)
            {
                _ = Task.Run(() => { try { serialPort.Dispose(); } catch { } });
                NotifyConnectionState(false);
                OpenFailed?.Invoke(this, new OpenFailedArgs(PortOpenError.OpenFailed, ex.Message));
            }
        }

        /// <summary>Uzavře port. Volej z UI nebo z Communication vrstvy.</summary>
        public void Close() => ClosePort();

        private void ClosePort()
        {
            if (_serialPort is null)
                return;

            var portToClose = _serialPort;
            _serialPort = null;

            _ = Task.Run(() =>
            {
                try { portToClose.Close(); } catch { }
                try { portToClose.Dispose(); } catch { }
            });

            if (_connectMenuItem.GetCurrentParent()?.InvokeRequired == true)
                _connectMenuItem.GetCurrentParent()!.Invoke(OnPortClosed);
            else
                OnPortClosed();
        }

        private void OnPortClosed()
        {
            NotifyConnectionState(false);
            PortClosed?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyConnectionState(bool isConnected)
            => ConnectionStateChanged?.Invoke(this, isConnected);

        private string? GetSelectedPort()
            => _portsComboBox.SelectedItem as string ?? _portTextBox.Text;

        private void RefreshPortsAndBind(string? preferredPort)
        {
            _ports.Clear();
            _ports.AddRange(SerialPort.GetPortNames().OrderBy(p => p));

            _portsComboBox.Items.Clear();
            _portsComboBox.Items.AddRange(_ports.Cast<object>().ToArray());

            if (!string.IsNullOrWhiteSpace(preferredPort) && _ports.Contains(preferredPort))
                _portsComboBox.SelectedItem = preferredPort;
            else if (_ports.Count > 0)
                _portsComboBox.SelectedIndex = 0;
        }

        public SerialPort GetOpenPort()
        {
            if (_serialPort?.IsOpen != true)
                throw new InvalidOperationException("COM port není otevřený.");

            return _serialPort;
        }

        public void Dispose()
        {
            _portsComboBox.DropDown -= PortsComboBox_DropDown;
            _portsComboBox.SelectedIndexChanged -= PortsComboBox_SelectedIndexChanged;
            _connectMenuItem.Click -= ConnectMenuItem_Click;

            try { _serialPort?.Close(); } catch { }
            try { _serialPort?.Dispose(); } catch { }
            _serialPort = null;

            GC.SuppressFinalize(this);
        }
    }
}