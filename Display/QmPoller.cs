using F28x_Project.Interfaces;
using F28x_Project.ResponseDTO;
using ScottPlot.WinForms;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F28x_Project.Display
{
    internal sealed class QmPoller
    {
        private const int WarmupSkipTicks = 2; // přeskočí prvních ~400ms po připojení

        private readonly Communication _communication;
        private readonly Label _readingValueLabel;
        private readonly Label _unitLabel;
        private readonly Label _stateLabel;
        private readonly MeasurementGraphRenderer _graphRenderer;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 200 };
        private readonly Stopwatch _stopwatch = new();
        private ILocalizationProvider? _localization;
        private bool _isPolling;
        private int _warmupRemaining;

        public QmPoller(
            Communication communication,
            Label readingValueLabel,
            Label unitLabel,
            Label stateLabel,
            FormsPlot formsPlot,
            GraphMode graphMode)
        {
            _communication = communication;
            _readingValueLabel = readingValueLabel;
            _unitLabel = unitLabel;
            _stateLabel = stateLabel;
            _graphRenderer = new MeasurementGraphRenderer(formsPlot, graphMode);
            _timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            _warmupRemaining = WarmupSkipTicks;
            _stopwatch.Restart();
            _graphRenderer.Reset();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _stopwatch.Stop();
        }

        public void ApplyLocalization(ILocalizationProvider provider) => _localization = provider;

        public void SetGraphMode(GraphMode mode)
        {
            _graphRenderer.SetGraphMode(mode);
            _stopwatch.Restart();
        }

        private string Translate(string key) => _localization?.Get(key) ?? key;

        private async void Timer_Tick(object? sender, EventArgs e)
        {
            if (_isPolling)
                return;

            _isPolling = true;
            try
            {
                var result = await Task.Run(() => _communication.QmCommand());

                if (result is null || result.Ack != "0")
                    return;

                if (_warmupRemaining > 0)
                {
                    _warmupRemaining--;
                    UpdateDisplayOnly(result.Response); // zobrazíme, ale do grafu nepíšeme
                    return;
                }

                UpdateDisplay(result.Response);
            }
            finally
            {
                _isPolling = false;
            }
        }

        private void UpdateDisplay(QmResponse response)
        {
            switch (response.State)
            {
                case "OL": SetDisplay("OL  ", string.Empty, string.Empty); return;
                case "OL_MINUS": SetDisplay("-OL  ", string.Empty, string.Empty); return;
                case "BLANK": SetDisplay(string.Empty, string.Empty, string.Empty); return;
                case "INVALID": SetDisplay("-----", string.Empty, string.Empty); return;
                case "OPEN_TC": SetDisplay("-----", string.Empty, Translate("State.OpenTC")); return;
                case "DISCHARGE": SetDisplay("-----", string.Empty, Translate("State.Discharge")); return;
            }

            var (scaledValue, displayUnit) = QmFormatter.ScaleReading(response.ReadingValue, response.Unit);
            var stateText = response.State == "NORMAL" ? string.Empty : response.State;

            SetDisplay(QmFormatter.FormatReading(scaledValue), displayUnit, stateText);
            _graphRenderer.AddPoint(_stopwatch.Elapsed.TotalSeconds, scaledValue);
        }

        private void UpdateDisplayOnly(QmResponse response)
        {
            if (response.State is "OL" or "OL_MINUS" or "BLANK" or "INVALID" or "OPEN_TC" or "DISCHARGE")
                return;

            var (scaledValue, displayUnit) = QmFormatter.ScaleReading(response.ReadingValue, response.Unit);
            var stateText = response.State == "NORMAL" ? string.Empty : response.State;
            SetDisplay(QmFormatter.FormatReading(scaledValue), displayUnit, stateText);
        }

        private void SetDisplay(string reading, string unit, string state)
        {
            _readingValueLabel.Text = reading;
            _unitLabel.Text = unit;
            _stateLabel.Text = state;
        }
    }
}