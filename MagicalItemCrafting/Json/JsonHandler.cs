using Newtonsoft.Json;
using MagicalCrafting.Core;
using MagicalCrafting.Enums;
using MagicalCrafting.Records;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace MagicalCrafting.Json;

public static class JsonHandler
{
    public static List<Ingredient> LoadItems(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            
            var items = JsonConvert.DeserializeObject<List<Ingredient>>(json);

            return items ?? new List<Ingredient>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the JSON file: {ex.Message}");
            return new List<Ingredient>();
        }
    }
}