using System.Net.WebSockets;
using System.Runtime.InteropServices;
using MagicalCrafting.Core;
using MagicalCrafting.Enums;
using MagicalCrafting.Json;
using MagicalCrafting.Records;

namespace MagicalCrafting
{
    public static class Program
    {
        private static CraftingTable _craftingTable = new();

        public static void Main()
        {
            string filePath = @$"Json/ItemList.json";

            List<Ingredient> items = JsonHandler.LoadItems(filePath);
            bool isGameRunning = true;
            Console.WriteLine();
            Console.WriteLine("----- Welcome to the Magical Crafting System -----");

            while(isGameRunning)
            {
                Console.WriteLine();
                Console.WriteLine("\t1. Add Ingredient to Slot 1 \n\t2. Add Ingredient to Slot 2 \n\t3. Attempt Crafting \n\t4. Clear Crafting Table \n\t5. Show Current Stats \n\t6. Show Ingredients list \n\t7. Show Items \n\t8. Exit");

                if(!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Please enter a number between 1 and 5.");
                    continue;
                }

                switch(input)
                {
                    case 1:
                        AddItemToSlot(items, 1);
                        break;
                    case 2:
                        AddItemToSlot(items, 2);
                        break;
                    case 3:
                        AttemptCrafting();
                        break;
                    case 4:
                        _craftingTable = new CraftingTable();
                        Console.WriteLine("\tCrafting Table Cleared!");
                        break;
                    case 5:
                        Console.WriteLine(Player.ShowStats());
                        break;
                    case 6:
                        Ingredient.ShowIngredients();
                        break;
                    case 7:
                        ShowItems(items);
                        break;
                    case 8:
                        Console.WriteLine("Thank you for using the Magical Crafting Table!");
                        isGameRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Enter a number between 1 and 5.");
                        break;
                }
            }
        }

        private static void ShowItems(List<Ingredient> items)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("===== Magical Items List =====");

            if (items.Count == 0)
            {
                Console.WriteLine("No Items Available");
                return;
            }

            foreach (var i in items)
            {
                Console.WriteLine($"-> Name: {i.Name}");
                Console.WriteLine($"  Element: {i.Element}");
                Console.WriteLine("------------------------");
            }
        }

        private static void AttemptCrafting()
        {
            var result = _craftingTable.AttemptCrafting();

            if(result.Success)
            {
                Player.totalCraftsMade++;
            }
            Console.WriteLine();
            Console.WriteLine($"\tCrafting Results:\n\t\tItem: {result.ItemName}\n\t\tQuality: {result.Quality:P0}\n\t\tSuccess: {result.Success}\n\t\tSpecial Effect: {result.Message}");
        }

        private static void AddItemToSlot(List<Ingredient> items, int slot)
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
    }
}