using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using F28x_Project.Interfaces;

namespace F28x_Project
{
    internal sealed class ComPortManager
    {
        private const int OpenTimeoutMs = 500;
        private readonly ToolStripComboBox _portsComboBox;
        private readonly ToolStripTextBox _portTextBox;
        private readonly ToolStripMenuItem _connectMenuItem;
        private readonly ISettings _settings;
        private readonly List<string> _ports = new();
        private SerialPort? _serialPort;

        public event EventHandler? PortOpened;
        public event EventHandler? PortClosed;

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

            UpdateConnectButtonState(false);
        }

        public IReadOnlyList<string> Ports => _ports;

        public void Initialize()
        {
            RefreshPortsAndBind(_settings.Port);
        }

        private void PortsComboBox_DropDown(object? sender, System.EventArgs e)
        {
            RefreshPortsAndBind(_portsComboBox.SelectedItem as string);
        }

        private void PortsComboBox_SelectedIndexChanged(object? sender, System.EventArgs e)
        {
            if (_portsComboBox.SelectedItem is string portName)
            {
                _portTextBox.Text = portName;
                _settings.UpdatePort(portName);
            }
        }

        private void ConnectMenuItem_Click(object? sender, EventArgs e)
        {
            if (_serialPort?.IsOpen == true)
            {
                ClosePort();
                return;
            }

            var portName = GetSelectedPort();
            if (string.IsNullOrWhiteSpace(portName))
            {
                return;
            }

            OpenPort(portName);
        }

        private void OpenPort(string portName)
        {
            ClosePort();

            try
            {
                _serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

                var openTask = Task.Run(() => _serialPort.Open());
                if (!openTask.Wait(OpenTimeoutMs))
                {
                    _serialPort.Dispose();
                    _serialPort = null;
                    throw new TimeoutException($"{portName} opening timeout.");
                }

                UpdateConnectButtonState(true);
                PortOpened?.Invoke(this, EventArgs.Empty);
            }
            catch (UnauthorizedAccessException)
            {
                _serialPort?.Dispose();
                _serialPort = null;

                UpdateConnectButtonState(false);
                PortClosed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                _serialPort?.Dispose();
                _serialPort = null;

                UpdateConnectButtonState(false);
                PortClosed?.Invoke(this, EventArgs.Empty);
                throw;
            }
        }

        private void ClosePort()
        {
            if (_serialPort is null)
            {
                return;
            }

            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }

            UpdateConnectButtonState(false);

            PortClosed?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateConnectButtonState(bool isConnected)
        {
            _connectMenuItem.BackColor = isConnected ? Color.LightGreen : SystemColors.Control;
        }

        private string? GetSelectedPort()
        {
            return _portsComboBox.SelectedItem as string ?? _portTextBox.Text;
        }

        private void RefreshPortsAndBind(string? preferredPort)
        {
            _ports.Clear();
            _ports.AddRange(SerialPort.GetPortNames().OrderBy(p => p));

            _portsComboBox.Items.Clear();
            _portsComboBox.Items.AddRange(_ports.Cast<object>().ToArray());

            if (!string.IsNullOrWhiteSpace(preferredPort) && _ports.Contains(preferredPort))
            {
                _portsComboBox.SelectedItem = preferredPort;
            }
            else if (_ports.Count > 0)
            {
                _portsComboBox.SelectedIndex = 0;
            }
        }
        public SerialPort GetOpenPort()
        {
            if (_serialPort?.IsOpen != true)
            {
                throw new InvalidOperationException("COM port is not open.");
            }

            return _serialPort;
        }
    }
}