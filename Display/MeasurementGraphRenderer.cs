using F28X_Toolset.Interfaces;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;

namespace F28X_Toolset.Display
{
    internal sealed class MeasurementGraphRenderer
    {
        private const double MinYSpan = 0.005; // minimální viditelné rozpětí Y osy
        private const double ScrollWindowSeconds = 60.0;
        private const int MaxContinuousPoints = 54_000; // 3 hodiny @ 200 ms

        private readonly FormsPlot _formsPlot;
        private readonly List<double> _xValues = new();
        private readonly List<double> _yValues = new();
        private GraphMode _graphMode;

        // Průběžné min/max — O(1) místo O(n) Min()/Max() každý tick (#4)
        private double _yMin = double.MaxValue;
        private double _yMax = double.MinValue;

        public MeasurementGraphRenderer(FormsPlot formsPlot, GraphMode graphMode)
        {
            _formsPlot = formsPlot;
            _graphMode = graphMode;
            InitializePlot();
        }

        public void SetGraphMode(GraphMode mode)
        {
            _graphMode = mode;
            Reset();
        }

        public void Reset()
        {
            _xValues.Clear();
            _yValues.Clear();
            _yMin = double.MaxValue;
            _yMax = double.MinValue;
            InitializePlot();
        }

        public void AddPoint(double time, double value)
        {
            _xValues.Add(time);
            _yValues.Add(value);

            // Aktualizace min/max — O(1)
            if (value < _yMin) _yMin = value;
            if (value > _yMax) _yMax = value;

            TrimData(time);
            Render(time);
        }

        private void TrimData(double currentTime)
        {
            if (_graphMode == GraphMode.Scrolling)
            {
                var cutoff = currentTime - ScrollWindowSeconds;
                var removeCount = 0;
                while (removeCount < _xValues.Count && _xValues[removeCount] < cutoff)
                    removeCount++;

                if (removeCount > 0)
                {
                    _xValues.RemoveRange(0, removeCount);
                    _yValues.RemoveRange(0, removeCount);
                    // Po ořezu přepočítáme min/max ze zbylých dat — nastane jen občas
                    RecalcMinMax();
                }
            }
            else
            {
                if (_xValues.Count > MaxContinuousPoints)
                {
                    var excess = _xValues.Count - MaxContinuousPoints;
                    _xValues.RemoveRange(0, excess);
                    _yValues.RemoveRange(0, excess);
                    RecalcMinMax();
                }
            }
        }

        /// <summary>
        /// Přepočet min/max po ořezu dat. Volá se jen při mazání bodů, ne každý tick.
        /// </summary>
        private void RecalcMinMax()
        {
            if (_yValues.Count == 0)
            {
                _yMin = double.MaxValue;
                _yMax = double.MinValue;
                return;
            }

            _yMin = double.MaxValue;
            _yMax = double.MinValue;
            foreach (var v in _yValues)
            {
                if (v < _yMin) _yMin = v;
                if (v > _yMax) _yMax = v;
            }
        }

        private void InitializePlot()
        {
            _formsPlot.Plot.Clear();
            _formsPlot.Plot.Axes.SetLimits(left: 0, right: ScrollWindowSeconds, bottom: -5, top: 5);
            _formsPlot.Plot.Axes.Bottom.Label.Text = "t [s]";
            _formsPlot.Refresh();
        }

        private void Render(double t)
        {
            double xMin, xMax;
            if (_graphMode == GraphMode.Scrolling)
            {
                xMax = Math.Max(ScrollWindowSeconds, t + 5);
                xMin = t > ScrollWindowSeconds ? t - ScrollWindowSeconds : 0;
            }
            else
            {
                xMin = 0;
                xMax = Math.Max(ScrollWindowSeconds, t + 5);
            }

            var yMin = _yValues.Count > 0 ? _yMin : -5;
            var yMax = _yValues.Count > 0 ? _yMax : 5;

            // Zajistí minimální rozpětí — šum pod 0,05 nebude opticky dominovat
            var midY = (yMin + yMax) / 2.0;
            var halfSpan = Math.Max((yMax - yMin) / 2.0, MinYSpan / 2.0);

            var yMargin = halfSpan * 0.1;

            _formsPlot.Plot.Clear();
            _formsPlot.Plot.Add.ScatterLine(_xValues.ToArray(), _yValues.ToArray());
            _formsPlot.Plot.Axes.SetLimits(
                left: xMin,
                right: xMax,
                bottom: yMin - yMargin,
                top: yMax + yMargin);
            _formsPlot.Plot.Axes.Bottom.Label.Text = "t [s]";
            _formsPlot.Refresh();
        }
    }
}