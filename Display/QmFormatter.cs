using System;
using System.Globalization;

namespace F28x_Project.Display
{
    internal static class QmFormatter
    {
        public static string FormatReading(double value)
        {
            if (value == 0)
                return (0.0).ToString("F4", CultureInfo.CurrentCulture);

            var abs = Math.Abs(value);
            var intDigits = Math.Max(1, (int)Math.Floor(Math.Log10(abs)) + 1);
            var decimalPlaces = Math.Max(0, 5 - intDigits);

            return value.ToString("F" + decimalPlaces, CultureInfo.CurrentCulture);
        }

        ///// <summary>
        ///// Zaokrouhlí hodnotu na stejné rozlišení jako <see cref="FormatReading"/>.
        ///// Hodnoty pod rozlišením displeje (např. 2E-05 při zobrazení F4) se tak snapnou na 0.
        ///// </summary>
        //public static double SnapToDisplayResolution(double value)
        //{
        //    if (value == 0) return 0;

        //    var abs = Math.Abs(value);
        //    var intDigits = Math.Max(1, (int)Math.Floor(Math.Log10(abs)) + 1);
        //    var decimalPlaces = Math.Max(0, 5 - intDigits);

        //    return Math.Round(value, decimalPlaces);
        //}

        /// <summary>
        /// Pro OHM přepočítá na Ω / kΩ / MΩ. Pro ostatní mapuje název přes <see cref="MapUnit"/>.
        /// </summary>
        public static (double Value, string Unit) ScaleReading(double value, string unit)
        {
            if (unit == "OHM")
            {
                var abs = Math.Abs(value);
                if (abs >= 1_000_000) return (value / 1_000_000, "MΩ");
                if (abs >= 1_000) return (value / 1_000, "kΩ");
                return (value, "Ω");
                // "OHM" vstup nikdy nedosáhne MapUnit — větev tam odstraněna (#14)
            }

            return (value, MapUnit(unit));
        }

        public static string MapUnit(string unit) => unit switch
        {
            "NONE" => string.Empty,
            "VAC_PLUS_DC" => "VAC+DC",
            "AAC_PLUS_DC" => "AAC+DC",
            "CEL" => "°C",
            "FAR" => "°F",
            "SIE" => "nS",
            "PCT" => "%",
            "CREST_FACTOR" => "CF",
            _ => unit
        };
    }
}