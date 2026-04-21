using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using F28x_Project.Interfaces;

namespace F28x_Project
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
        }

        public SettingsFileManager()
        {
            var fileDirectory = ResolveFileDirectory();
            _settingsFilePath = Path.Combine(fileDirectory, "settings.json");
            _current = LoadFromDisk();
        }

        public string? Port => _current.Port;
        public string? Language => _current.Language;

        public void UpdatePort(string? port)
        {
            if (string.Equals(_current.Port, port, StringComparison.Ordinal))
            {
                return;
            }

            _current = _current with { Port = port };
            SaveToDisk(_current);
        }

        public void UpdateLanguage(string? language)
        {
            if (string.Equals(_current.Language, language, StringComparison.Ordinal))
            {
                return;
            }

            _current = _current with { Language = language };
            SaveToDisk(_current);
        }

        private SettingsData LoadFromDisk()
        {
            if (!File.Exists(_settingsFilePath))
            {
                var data = new SettingsData();
                SaveToDisk(data);
                return data;
            }

            try
            {
                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
            }
            catch
            {
                var data = new SettingsData();
                SaveToDisk(data);
                return data;
            }
        }

        private void SaveToDisk(SettingsData data)
        {
            var json = JsonSerializer.Serialize(data, SerializerOptions);
            File.WriteAllText(_settingsFilePath, json);
        }

        private static string ResolveFileDirectory()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null)
            {
                if (directory.EnumerateFiles("*.csproj").Any())
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            return AppContext.BaseDirectory;
        }
    }
}