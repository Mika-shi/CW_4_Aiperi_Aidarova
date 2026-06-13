using System.Text.Json;
using CW_4.Models;
namespace CW_4.Services;

public class PetStorageService
{
    private readonly string _filePath = Path.Combine("Data", "pets.json");
    public void Save(List<Pet> pets)
    {
        Directory.CreateDirectory("Data");
        string json = JsonSerializer.Serialize(
            pets,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );
        File.WriteAllText(_filePath, json);
    }

    public List<Pet> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Pet>();
        }
        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Pet>();
        }
        List<Pet>? pets = JsonSerializer.Deserialize<List<Pet>>(json);
        return pets ?? new List<Pet>();
    }
}