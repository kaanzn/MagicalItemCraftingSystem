namespace MagicalCrafting.Core
{
    public class CraftingResult
    {
        public string Result;
        public decimal Quality;
        public bool Success;
        public string Message;

        public CraftingResult(string result, decimal quality, bool success, string message)
        {
            Result = result;
            Quality = quality;
            Success = success;
            Message = message;
        }
    }
}