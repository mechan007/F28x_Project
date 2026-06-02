using System;
using System.Threading;
using System.Threading.Tasks;
using F28X_Toolset.Parsers;
using F28X_Toolset.ResponseDTO;

namespace F28X_Toolset
{
    internal sealed class Communication
    {
        private const int ReadTimeoutMs = 2000;
        private const int IdCommandTimeoutMs = 3000; // tvrdý strop — ReadTimeout není spolehlivý
        private const string ExpectedModelPrefix = "FLUKE";

        private readonly ComPortManager _comPortManager;

        public event EventHandler<IdResult>? DeviceConnected;
        public event EventHandler? DeviceDisconnected;
        public event EventHandler? DeviceNotFound;

        public Communication(ComPortManager comPortManager)
        {
            _comPortManager = comPortManager;
            _comPortManager.PortOpened += ComPortManager_PortOpened;
            _comPortManager.PortClosed += ComPortManager_PortClosed;
        }

        private void ComPortManager_PortOpened(object? sender, EventArgs e)
        {
            using var cts = new CancellationTokenSource(IdCommandTimeoutMs);

            IdResult? result = null;
            try
            {
                var idTask = Task.Run(IdCommand, cts.Token);
                result = idTask.Wait(IdCommandTimeoutMs) ? idTask.Result : null;
            }
            catch { result = null; }

            var isValidDevice = result is not null
                && result.Model.StartsWith(ExpectedModelPrefix, StringComparison.OrdinalIgnoreCase);

            if (isValidDevice)
                DeviceConnected?.Invoke(this, result!);
            else
            {
                DeviceNotFound?.Invoke(this, EventArgs.Empty);
                _comPortManager.Close();
            }
        }

        private void ComPortManager_PortClosed(object? sender, EventArgs e)
            => DeviceDisconnected?.Invoke(this, EventArgs.Empty);

        private IdResult? IdCommand()
        {
            try
            {
                var port = _comPortManager.GetOpenPort();
                var origNewLine = port.NewLine;
                var origTimeout = port.ReadTimeout;
                try
                {
                    port.NewLine = "\r";
                    port.ReadTimeout = ReadTimeoutMs;
                    port.DiscardInBuffer();
                    port.WriteLine("id\r");

                    var ack = port.ReadLine().Trim();
                    if (ack != "0")
                        return null;

                    var dataLine = port.ReadLine().Trim();
                    var parts = dataLine.Split(',', 3, StringSplitOptions.TrimEntries);

                    return new IdResult(
                        Ack: ack,
                        Model: parts.Length > 0 ? parts[0] : string.Empty,
                        Version: parts.Length > 1 ? parts[1] : string.Empty,
                        Serial: parts.Length > 2 ? parts[2] : string.Empty);
                }
                finally
                {
                    port.NewLine = origNewLine;
                    port.ReadTimeout = origTimeout;
                }
            }
            catch { return null; }
        }

        public QmResult? QmCommand()
        {
            try
            {
                var port = _comPortManager.GetOpenPort();
                var origNewLine = port.NewLine;
                var origTimeout = port.ReadTimeout;
                try
                {
                    port.NewLine = "\r";
                    port.ReadTimeout = ReadTimeoutMs;
                    port.DiscardInBuffer();
                    port.WriteLine("qm\r");

                    var ack = port.ReadLine().Trim();
                    var dataLine = port.ReadLine().Trim();
                    var response = QmResponseParser.Parse(dataLine);

                    return new QmResult(ack, response);
                }
                finally
                {
                    port.NewLine = origNewLine;
                    port.ReadTimeout = origTimeout;
                }
            }
            catch { return null; }
        }
    }
}