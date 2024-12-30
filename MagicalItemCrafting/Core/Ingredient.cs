using System.Diagnostics.Contracts;
using MagicalCrafting.Enums;
using MagicalCrafting;

namespace MagicalCrafting.Core
{
    public class Ingredient
    {
        public static List<Ingredient> ingredients = new();
        public string Name;
        public Element Element; 
        public int Potency;
        public IngredientType Type;
        public Rarity Rarity;

        public static void ShowIngredients()
        {
            if (ingredients.Count <= 0)
            {
                Console.WriteLine("There are no ingredients");
            }
            foreach ( var i in ingredients)
            {
                Console.WriteLine();
                Console.WriteLine($"{i.Name}");
            }
        }

        public void CreateIngredient()
        {
            Console.WriteLine();

            Console.WriteLine("\tEnter Ingredient Name: ");
            string name = Console.ReadLine() ?? "Mystery Item"; // Same as an if statement that will check for null
            Console.WriteLine();

            // Element Selection (if input invalid, default to Spirit)
            Console.WriteLine("\tEnter Element: \n\t1. Fire \n\t2. Water \n\t3. Earth \n\t4. Air \n\t5. Spirit");


            Element element = Console.ReadLine() switch
            {
                "1" => Element.Fire,
                "2" => Element.Water,
                "3" => Element.Earth,
                "4" => Element.Air,
                "5" => Element.Spirit,
                _ => Element.Spirit,
            };

            //Get Potency value (1 - 10)
            Console.WriteLine(); 
            Console.WriteLine("\tEnter Potency Value (1 - 10): ");
            if(!int.TryParse(Console.ReadLine(), out int potency))
            {
                potency = 1;
            }

            potency = Math.Clamp(potency, 1, 10);

            //Ingredient Type Selection (Default to Crystal)
            Console.WriteLine();

            Console.WriteLine("\tEnter Ingredient Type: \n\t1. Crystal \n\t2. Herb \n\t3. Metal \n\t4. Essence");

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

            Console.WriteLine("\tSelect Rarity: \n\t1. Common\n\t2. Uncommon\n\t3. Rare\n\t4. Legendary");

            Rarity rarity = Console.ReadLine() switch
            {
                "1" => Rarity.Common,
                "2" => Rarity.Uncommon,
                "3" => Rarity.Rare,
                "4" => Rarity.Legendary,
                _ => Rarity.Common,
            };

            var ingredient = new Ingredient(name, element, potency, type, rarity);
            ingredient.AddToList(ingredient);
        }
        public void AddToList(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public Ingredient(string name, Element element, int potency, IngredientType type, Rarity rarity)
        {
            Name = name;
            Element = element;
            Potency = potency;
            Type = type;
            Rarity = rarity;
        }
    }   
}