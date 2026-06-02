using System.IO;
using System.Text.Json;
using F28X_Toolset.Interfaces;

namespace F28X_Toolset
{
    internal sealed class SettingsFileManager : ISettings
    {
        private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true };
        private readonly string _settingsFilePath;
        private SettingsData _current;

        private sealed record SettingsData
        {
            public string? Port { get; init; }
            public string? Language { get; init; }
            public GraphMode GraphMode { get; init; } = GraphMode.Scrolling;
        }

        public string? Port => _current.Port;
        public string? Language => _current.Language;
        public GraphMode GraphMode => _current.GraphMode;

        public SettingsFileManager()
        {
            _settingsFilePath = Path.Combine(ResolveFileDirectory(), "settings.json");
            _current = LoadFromDisk();
        }

        public void UpdatePort(string? port)
        {
            if (string.Equals(_current.Port, port, System.StringComparison.Ordinal))
                return;
            _current = _current with { Port = port };
            SaveToDisk(_current);
        }

        public void UpdateLanguage(string? language)
        {
            if (string.Equals(_current.Language, language, System.StringComparison.Ordinal))
                return;
            _current = _current with { Language = language };
            SaveToDisk(_current);
        }

        public void UpdateGraphMode(GraphMode mode)
        {
            if (_current.GraphMode == mode)
                return;
            _current = _current with { GraphMode = mode };
            SaveToDisk(_current);
        }

        private SettingsData LoadFromDisk()
        {
            if (!File.Exists(_settingsFilePath))
            {
                var fresh = new SettingsData();
                SaveToDisk(fresh);
                return fresh;
            }

            try
            {
                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
            }
            catch
            {
                var fresh = new SettingsData();
                SaveToDisk(fresh);
                return fresh;
            }
        }

        private void SaveToDisk(SettingsData data)
        {
            var json = JsonSerializer.Serialize(data, SerializerOptions);
            File.WriteAllText(_settingsFilePath, json);
        }

        private static string ResolveFileDirectory()
        {
#if DEBUG
            // V debug režimu ukládej vedle .csproj, aby settings přežily rebuild
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir is not null)
            {
                if (dir.GetFiles("*.csproj").Length > 0)
                    return dir.FullName;
                dir = dir.Parent;
            }
#endif
            // Release i fallback — vždy vedle EXE
            return AppContext.BaseDirectory;
        }
    }
}