namespace F28x_Project.ResponseDTO
{
    /// <summary>Výsledek příkazu QM — nahrazuje out parametry v QmCommand.</summary>
    internal sealed record QmResult(
        string Ack, 
        QmResponse Response);
}