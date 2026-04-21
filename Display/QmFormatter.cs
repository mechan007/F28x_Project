using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Pro odporové jednotky automaticky přepočítá hodnotu na Ω / kΩ / MΩ.
        /// Pro ostatní jednotky pouze mapuje název.
        /// </summary>
        public static (double Value, string Unit) ScaleReading(double value, string unit)
        {
            if (unit == "OHM")
            {
                var abs = Math.Abs(value);
                if (abs >= 1_000_000)
                    return (value / 1_000_000, "MΩ");
                if (abs >= 1_000)
                    return (value / 1_000, "kΩ");
                return (value, "Ω");
            }

            return (value, MapUnit(unit));
        }

        public static string MapUnit(string unit) => unit switch
        {
            "NONE" => string.Empty,
            "OHM" => "Ω",
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