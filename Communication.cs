using System;
using System.Windows.Forms;
using F28x_Project.Parsers;
using F28x_Project.ResponseDTO;

namespace F28x_Project
{
    internal sealed class Communication
    {
        private readonly ComPortManager _comPortManager;
        private readonly ToolStripStatusLabel _modelStatusLabel;
        private readonly ToolStripStatusLabel _serialStatusLabel;
        private readonly ToolStripStatusLabel _versionStatusLabel;
        private string _ackErrMsg = string.Empty;

        public Communication(
            ComPortManager comPortManager,
            ToolStripStatusLabel modelStatusLabel,
            ToolStripStatusLabel serialStatusLabel,
            ToolStripStatusLabel versionStatusLabel)
        {
            _comPortManager = comPortManager;
            _modelStatusLabel = modelStatusLabel;
            _serialStatusLabel = serialStatusLabel;
            _versionStatusLabel = versionStatusLabel;

            _comPortManager.PortOpened += ComPortManager_PortOpened;
            _comPortManager.PortClosed += ComPortManager_PortClosed;
        }

        public string AckErrMsg => _ackErrMsg;

        private void ComPortManager_PortOpened(object? sender, EventArgs e)
        {
            if (IdCommand(out var ack, out var model, out var version, out var serial))
            {
                CmdAck(ack, model, version, serial);
            }
        }
        private void ComPortManager_PortClosed(object? sender, EventArgs e)
        {
            _modelStatusLabel.Text = string.Empty;
            _serialStatusLabel.Text = string.Empty;
            _versionStatusLabel.Text = string.Empty;
            _ackErrMsg = string.Empty;
        }

        public bool IdCommand(out string ack,
                              out string model,
                              out string version,
                              out string serial)
        {
            ack = string.Empty;
            model = string.Empty;
            version = string.Empty;
            serial = string.Empty;

            try
            {
                var port = _comPortManager.GetOpenPort();
                var originalNewLine = port.NewLine;

                try
                {
                    port.NewLine = "\r";
                    port.DiscardInBuffer();

                    port.WriteLine("id\r");

                    ack = port.ReadLine().Trim();
                    var dataLine = port.ReadLine().Trim();

                    var parts = dataLine.Split(',', 3, StringSplitOptions.TrimEntries);

                    if (parts.Length > 0) model = parts[0];
                    if (parts.Length > 1) version = parts[1];
                    if (parts.Length > 2) serial = parts[2];

                    return true;
                }
                finally
                {
                    port.NewLine = originalNewLine;
                }
            }
            catch
            {
                return false;
            }
        }

        public void CmdAck(string ack, string model, string version, string serial)
        {
            _ackErrMsg = ack switch
            {
                "5" => "No data available",
                "2" => "Execution error",
                "1" => "Syntax error",
                "0" => string.Empty,
                _ => _ackErrMsg
            };

            if (!string.IsNullOrWhiteSpace(_ackErrMsg))
            {
                return;
            }

            _modelStatusLabel.Text = model;
            _serialStatusLabel.Text = serial;
            _versionStatusLabel.Text = version;
        }
        public bool QmCommand(out string ack, out QmResponse? response)
        {
            ack = string.Empty;
            response = null;
            try
            {
                var port = _comPortManager.GetOpenPort();
                var originalNewLine = port.NewLine;
                try
                {
                    port.NewLine = "\r";
                    port.DiscardInBuffer();

                    port.WriteLine("qm\r");

                    ack = port.ReadLine().Trim();
                    var dataLine = port.ReadLine().Trim();

                    response = QmResponseParser.Parse(dataLine);
                    return true;
                }
                finally
                {
                    port.NewLine = originalNewLine;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}