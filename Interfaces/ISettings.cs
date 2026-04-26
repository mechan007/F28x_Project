namespace F28x_Project.Interfaces
{
    internal interface ISettings
    {
        string? Port { get; }
        string? Language { get; }
        GraphMode GraphMode { get; }
        void UpdatePort(string? port);
        void UpdateLanguage(string? language);
        void UpdateGraphMode(GraphMode mode);
    }
}

