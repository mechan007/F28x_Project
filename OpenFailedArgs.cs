namespace F28X_Toolset
{
    internal enum PortOpenError { Timeout, OpenFailed }

    /// <summary>
    /// Data pro event <see cref="ComPortManager.OpenFailed"/>.
    /// <para><see cref="Error"/> určuje typ chyby — bez magic strings.</para>
    /// <para><see cref="Detail"/> je technický detail (název portu, systémová chyba…).</para>
    /// </summary>
    internal sealed record OpenFailedArgs(PortOpenError Error, string Detail);
}