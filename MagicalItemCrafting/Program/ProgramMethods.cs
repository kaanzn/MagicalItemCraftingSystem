using MagicalCrafting.Core;
using MagicalCrafting.Json;

namespace MagicalCrafting;

public static class ProgramMethods
{
    private static CraftingTable _craftingTable = new();
    private static List<CraftingResult> craftingResults = new();
    private static List<Ingredient>? items;
    internal static void ShowMenu()
    {
            Console.Clear();
            Console.WriteLine("------- The Magical Crafting Table -------");
            Console.WriteLine("\n\t1. Add Ingredient to Slot 1"); 
            Console.WriteLine("\t2. Add Ingredient to Slot 2");
            Console.WriteLine("\t3. Attempt Crafting");
            Console.WriteLine("\t4. Clear Crafting Table");
            Console.WriteLine("\t5. Show Player Stats");
            Console.WriteLine("\t6. Show Crafting Results");
            Console.WriteLine("\t7. Show Items");
            Console.WriteLine("\t8. Exit");
    }
    internal static bool HandleUserChoice(int input)
    {
            switch(input)
            {
                case 1: AddItemToSlot(items!, 1); break;
                case 2: AddItemToSlot(items!, 2); break;
                case 3: AttemptCrafting(); break;
                case 4: _craftingTable = new CraftingTable(); Console.WriteLine("\tCrafting Table Cleared!"); break;
                case 5: LevelTracker(); Console.WriteLine(Player.ShowStats()); break;
                case 6: ShowCraftingResults(); break;
                case 7: ShowItems(items!); break;
                case 8: Console.WriteLine("Thank you for using the Magical Crafting Table!"); return false;
                default: return true;
            }
        PressToContinue();
        return true;
    }
    internal static void ShowItems(List<Ingredient> items)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("===== Magical Items List =====");

        if (!items.Any())
        {
            Console.WriteLine("No Items Available");
            return;
        }

        foreach (var i in items)
        {
            Console.WriteLine($"-> Name: {i.Name}\n  Element: {i.Element}");
            Console.WriteLine("------------------------");
        }
    }
    internal static int GetUserInput()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int input) && input >= 1 && input <= 8)
            {
                return input;
            }
            Console.WriteLine("Invalid Choice. Enter a Number between 1 - 8.");

        }
    }
    internal static void PressToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press Any Key To Continue...");
        Console.ReadKey();
        Console.Clear();
    }
    internal static void LevelTracker()
    {
        switch (Player.totalCraftsMade)
        {
            case 0:
                Player.Level = 1;
                break;
            case 4:
                Player.Level = 2;
                break;
            case 8:
                Player.Level = 3;
                break;
            case 14:
                Player.Level = 4;
                break;
        }
    }
    internal static void ShowCraftingResults()
    {
        if (craftingResults.Count == 0)
        {
            Console.WriteLine("No Crafting Results Available");
            return;
        }
        foreach ( var i in craftingResults)
        {
            Console.WriteLine($"\n{i.Result} {i.Quality} {i.Success} {i.Message}");
        }
    }
    internal static void AddItemToSlot(List<Ingredient> items, int slot)
    {
        Console.WriteLine();
        Console.WriteLine("Enter the Name of the Ingredient: ");
        string input = Console.ReadLine()!;

        foreach (var i in items)
        {
            if (i.Name == input)
            {
                if(_craftingTable.AddIngredient(i, slot))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Successfully added {i.Name} to slot {slot}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Failed to add the Ingredient to slot {slot}");
                    break;
                }                    
            }
            else if (i.Name != input && i == items.Last())
            {
                Console.WriteLine($"Item {input} does not exist!");
                break;
            }
            else
            {
                continue;
            }
        }
    }
    internal static void AttemptCrafting()
    {
        var result = _craftingTable.AttemptCrafting();

        if(result.Success)
        {
            Player.totalCraftsMade++;
            craftingResults.Add(result);
        }
        Console.WriteLine();
        Console.WriteLine($"\tCrafting Results:\n\t\tItem: {result.Result}\n\t\tQuality: {result.Quality:P0}\n\t\tSuccess: {result.Success}\n\t\tSpecial Effect: {result.Message}");
    }
    internal static void LoadItems()
    {
        string filePath = @$"Json/ItemList.json";
        JsonHandler.LoadItems(filePath);
    }
}