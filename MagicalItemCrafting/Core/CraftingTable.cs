using MagicalCrafting.Enums;

namespace MagicalCrafting.Core
{
    public class CraftingTable
    {
        private const int MaxPotency = 10;
        private Ingredient? _slot1;
        private Ingredient? _slot2;

        public bool AddIngredient(Ingredient ingredient, int slot)
        {
            if(slot != 1 && slot != 2)
            {
                return false;
            }
            if(slot == 1)
            {
                _slot1 = ingredient;
            }
            else
            {
                _slot2 = ingredient;
            }
            return true;
        }
        public CraftingResult AttemptCrafting()
        {
            //Check if both slots have ingredients using pattern matching
            if(_slot1 is not Ingredient first || _slot2 is not Ingredient second)
            {
                return new CraftingResult("Craft Failed!", 0, false, "Both slots must contain Ingredients!");
            }

            //Check if ingredients can work together
            if(!CheckCompatibility())
            {
                return new CraftingResult("Craft Failed", 0, false, "These Ingredients are not compatible!");
            }
            
            //Calculate quality and determine the tier name
            decimal quality = CalculateQuality();
            string qualityTier = quality switch
            {
                <= 0.3m =>  "Poor",
                <= 0.6m => "Average",
                <= 0.8m => "Good",
                <= 0.9m => "Excellent",
                _ => "Masterwork"
            };

            //Determine what type of item we have created based on the ingredient types
            //Order does not matter
            string resultType = (first.Type, second.Type) switch
            {
                (IngredientType.Crystal, IngredientType.Herb)
                or (IngredientType.Herb, IngredientType.Crystal) => "Potion",

                (IngredientType.Metal, IngredientType.Crystal)
                or (IngredientType.Crystal, IngredientType.Metal) => "Amulet",

                (IngredientType.Herb, IngredientType.Essence)
                or (IngredientType.Essence, IngredientType.Herb) => "Elixir",

                _ => "Mixture"
            };

            //Check for special elemental effects
            string specialEffect = (first.Element, second.Element) switch
            {
                var (e1, e2) when (e1 == e2) => "Resonating Power",

                (Element.Fire, Element.Water) or
                (Element.Water, Element.Fire) => "Thermal Shock",

                (Element.Earth, Element.Air) or
                (Element.Air, Element.Earth) => "Dusty Whirlwind",

                (Element.Spirit, _) or
                (_, Element.Spirit) => "Spiritual Infusion",

                (Element.Fire, _) when first.Potency > second.Potency => "Burning",
                (Element.Water, _) when first.Potency > second.Potency => "Freezing",

                _ => "No special effect"
            };

            return new CraftingResult($"{qualityTier} {resultType}", quality, true, specialEffect);

        }
        private bool CheckCompatibility() //TODO: Turn each check into its own method
        {
            //We know that _slot1 and _slot2 are not null
            var first = _slot1!;
            var second = _slot2!;

            //Rule1: Combined potency cannot exceed 2 * MaxPotency
            if(first.Potency + second.Potency > 2 * MaxPotency)
            {
                return false;
            }

            //Rule2: Opposing elements require high potency (>=8) to combine
            //Only Master Crafters can handle volatile combinations
            //Note: Spirit has no opposing elements
            if((first.Element == Element.Fire && second.Element == Element.Water) ||
               (first.Element == Element.Water && second.Element == Element.Fire) ||
               (first.Element == Element.Earth && second.Element == Element.Air) ||
               (first.Element == Element.Air && second.Element == Element.Earth))
            {
                if(first.Potency < 8 || second.Potency < 8)
                {
                    return false;
                }
            }

            // Rule3: Legendary items cannot be paired with Common items
            if((first.Rarity == Rarity.Legendary && second.Rarity == Rarity.Common) ||
               (first.Rarity == Rarity.Common && second.Rarity == Rarity.Legendary))
            {
                return false;
            }

            //Rule4: Strong Potency (8 - 10) cannot be paired with weak Potency (1 - 3)
            bool isFirstStrong = first.Potency >= 8;
            bool isFirstWeak = first.Potency <= 3;
            bool isSecondStrong = second.Potency >= 8;
            bool isSecondWeak = second.Potency <= 3;
            if((isFirstStrong && isSecondWeak) || (isFirstWeak && isSecondStrong))
            {
                return false;
            }

            //All compatibility checks have passed
            return true;
        }
        private decimal CalculateQuality()
        {
            var first = _slot1!;
            var second = _slot2!;

            //Calculate base quality: (potency1 + potency2) / (2 * MaxPotency)
            decimal baseQuality = (first.Potency + second.Potency) / (2.0m * MaxPotency);

            //Add rarity bonuses using switch expression pattern matching
            decimal rarityBonus = (first.Rarity, second.Rarity) switch
            {
                (Rarity.Legendary, Rarity.Legendary) => 0.5m,
                (Rarity.Legendary, _) or (_, Rarity.Legendary) => 0.3m,
                (Rarity.Rare, Rarity.Rare) => 0.2m,
                var (r1, r2) when (r1 == r2) => 0.1m, // Matching rarities
                _ => 0.0m // No rarity bonus
            };

            //Cap the final at 1.0
            return Math.Min(baseQuality + rarityBonus, 1.0m);



        }
    }
}