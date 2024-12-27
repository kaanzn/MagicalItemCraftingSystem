namespace MagicalCrafting.Records
{
    public record CraftingResult(
        string ItemName,
        decimal Quality,
        bool Success,
        string Message
    );
}