using MagicalCrafting.Core;
using MagicalCrafting.Enums;
using MagicalCrafting.Records;

namespace MagicalCrafting
{
    public static class Program
    {
        private static CraftingTable _craftingTable = new();

        public static void Main()
        {
            bool isGameRunning = true;
            Console.WriteLine("----- Welcome to the Magical Crafting System -----");

            while(isGameRunning)
            {
                Console.WriteLine(@"
                1. Add Ingredient to Slot 1
                2. Add Ingredient to Slot 2
                3. Attempt Crafting
                4. Clear Crafting Table
                5. Exit
                
                ");

                if(!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Please enter a number between 1 and 5.");
                    continue;
                }

                switch(input)
                {
                    case 1:
                        AddIngredient(input);
                        break;
                    case 2:
                        AddIngredient(input);
                        break;
                    case 3:
                        AttemptCrafting();
                        break;
                    case 4:
                        _craftingTable = new CraftingTable();
                        break;
                    case 5:
                        Console.WriteLine("Thank you for using the Magical Crafting Table!");
                        isGameRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Enter a number between 1 and 5.");
                        break;
                }
            }
        }

        private static void AttemptCrafting()
        {
            var result = _craftingTable.AttemptCrafting();

            Console.WriteLine($@"
                Crafting Results:
                    Item: {result.ItemName}
                    Quality: {result.Quality:P0}
                    Success: {result.Success}
                    Special Effect: {result.Message}
            ");
        }

        private static void AddIngredient(int slot)
        {
            Console.WriteLine();

            Console.WriteLine("Enter Ingredient Name: ");
            string name = Console.ReadLine() ?? "Mystery Item"; // Same as an if statement that will check for null

            // Element Selection (if input invalid, default to Spirit)
            Console.WriteLine(@"
                Enter Element: 
                    1. Fire
                    2. Ice
                    3. Earth
                    4. Air
                    5. Spirit
                
            ");


            Element element = Console.ReadLine() switch
            {
                "1" => Element.Fire,
                "2" => Element.Ice,
                "3" => Element.Earth,
                "4" => Element.Air,
                "5" => Element.Spirit,
                _ => Element.Spirit,
            };

            //Get Potency value (1 - 10)
            Console.WriteLine(); 
            Console.WriteLine("Enter Potency Value (1 - 10): ");
            if(!int.TryParse(Console.ReadLine(), out int potency))
            {
                potency = 1;
            }

            potency = Math.Clamp(potency, 1, 10);

            //Ingredient Type Selection (Default to Crystal)
            Console.WriteLine();

            Console.WriteLine(@"
                Enter Ingredient Type: 
                    1. Crystal
                    2. Herb
                    3. Metal
                    4. Essence
            ");

            IngredientType type = Console.ReadLine() switch
            {
                "1" => IngredientType.Crystal,
                "2" => IngredientType.Herb,
                "3" => IngredientType.Metal,
                "4" => IngredientType.Essence,
                _ => IngredientType.Crystal,

            };

            //Rarity Selection (Default to Common)
            Console.WriteLine();

            Console.WriteLine(@"
                Select Rarity: 
                    1. Common
                    2. Uncommon
                    3. Rare
                    4. Legendary
            ");

            Rarity rarity = Console.ReadLine() switch
            {
                "1" => Rarity.Common,
                "2" => Rarity.Uncommon,
                "3" => Rarity.Rare,
                "4" => Rarity.Legendary,
                _ => Rarity.Common,
            };

            var ingredient = new Ingredient(name, element, potency, type, rarity);

            if(_craftingTable.AddIngredient(ingredient, slot))
            {
                Console.WriteLine();
                Console.WriteLine($"Successfully added {name} to slot {slot}");
            }
            else
            {
                Console.WriteLine($"Failed to add the Ingredient to slot {slot}");
            }


        }
    }
}