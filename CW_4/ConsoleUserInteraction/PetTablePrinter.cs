using CW_4.Comparers;
using CW_4.Helpers;
using CW_4.Models;
namespace CW_4.ConsoleUserInteraction;
public class PetTablePrinter
{
    public void Print(List<Pet> pets)
    {
        if (pets.Count == 0)
        {
            Console.WriteLine("Список животных пуст.");
            return;
        }
        pets.Sort(new PetLifeLevelComparer());
        Console.WriteLine("№ | Животное | Имя | Возраст | Сытость | Настроение | Здоровье | Средний уровень жизни");
        for (int i = 0; i < pets.Count; i++)
        {
            Pet pet = pets[i];

            Console.WriteLine(
                $"{i + 1} | {pet.Type} | {pet.Name} | {AgeHelper.FormatAge(pet.AgeInDays)} | {pet.Satiety} | {pet.Mood} | {pet.Health} | {pet.AverageLifeLevel:F1}"
            );
        }
    }
}
