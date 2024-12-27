using System.Diagnostics.Contracts;
using MagicalCrafting.Enums;

namespace MagicalCrafting.Records
{
    public record Ingredient(
        string Name,
        Element Element, 
        int Potency, 
        IngredientType Type,
        Rarity Rarity
        );
}