using System.Globalization;
using F28X_Toolset.ResponseDTO;

namespace F28X_Toolset.Parsers
{
    internal static class QmResponseParser
    {
        public static QmResponse Parse(string dataLine)
        {
            var parts = dataLine.Split(',', 4, StringSplitOptions.TrimEntries);

            return new QmResponse(
                ReadingValue: parts.Length > 0 ? ParseDouble(parts[0]) : 0,
                Unit: parts.Length > 1 ? parts[1] : string.Empty,
                State: parts.Length > 2 ? parts[2] : string.Empty,
                Attribute: parts.Length > 3 ? parts[3] : string.Empty);
        }

        private static double ParseDouble(string value)
            => double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)
                ? result
                : 0;
    }
}