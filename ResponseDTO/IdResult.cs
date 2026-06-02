namespace F28X_Toolset.ResponseDTO
{
    internal sealed record IdResult(
        string Ack,
        string Model,
        string Version,
        string Serial);
}
