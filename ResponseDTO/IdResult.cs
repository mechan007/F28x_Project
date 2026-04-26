namespace F28x_Project.ResponseDTO
{
    internal sealed record IdResult(
        string Ack,
        string Model,
        string Version,
        string Serial);
}
