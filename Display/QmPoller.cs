using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using F28x_Project.ResponseDTO;

namespace F28x_Project.Display
{
    internal sealed class QmPoller
    {
        private readonly Communication _comunication;
        private readonly Label _readingValueLabel;
        private readonly Label _unitLabel;
        private readonly Label _stateLabel;
        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 200 };

        public QmPoller(
            Communication comunication,
            Label readingValueLabel,
            Label unitLabel,
            Label stateLabel)
        {
            _comunication = comunication;
            _readingValueLabel = readingValueLabel;
            _unitLabel = unitLabel;
            _stateLabel = stateLabel;

            _timer.Tick += Timer_Tick;
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (!_comunication.QmCommand(out var ack, out var response) || response is null)
                return;

            if (ack != "0")
                return;

            if (response.State is "OL")
            {
                _readingValueLabel.Text = "OL  ";
                _stateLabel.Text = string.Empty;
                return;
            }

            if (response.State is "OL_MINUS")
            {
                _readingValueLabel.Text = "-OL  ";
                _stateLabel.Text = string.Empty;
                return;
            }

            if (response.State is "BLANK")
            {
                _readingValueLabel.Text = string.Empty;
                _stateLabel.Text = string.Empty;
                return;
            }

            if (response.State is "INVALID")
            {
                _readingValueLabel.Text = "-----";
                return;
            }

            if (response.State is "OPEN_TC")
            {
                _readingValueLabel.Text = "-----";
                _stateLabel.Text = "Open thermocouple!";
                return;
            }

            if (response.State is "DISCHARGE")
            {
                _readingValueLabel.Text = "-----";
                _stateLabel.Text = "Discharge error in capacitance!";
                return;
            }

            var (scaledValue, displayUnit) = QmFormatter.ScaleReading(response.ReadingValue, response.Unit);

            _readingValueLabel.Text = QmFormatter.FormatReading(scaledValue);
            _unitLabel.Text = displayUnit;
            _stateLabel.Text = response.State == "NORMAL" ? string.Empty : response.State;
        }
    }
}
