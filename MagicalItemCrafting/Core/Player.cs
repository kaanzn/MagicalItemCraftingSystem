namespace MagicalCrafting.Core
{
    public static class Player
    {
        public static int Level = 1;
        public static int totalCraftsMade = 0;

        public static string ShowStats()
        {
            return $"\nCurrent Stats >\n\tLevel:\t\t{Player.Level}\n\tTotal Crafts:   {Player.totalCraftsMade}\n";
        }
    }
}