namespace F28X_Toolset.ResponseDTO
{
    internal sealed record QmResponse(
        double ReadingValue,
        string Unit,
        string State,
        string Attribute);
}
