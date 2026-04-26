namespace F28x_Project.ResponseDTO
{
    internal sealed record QmResponse(
        double ReadingValue,
        string Unit,
        string State,
        string Attribute);
}
