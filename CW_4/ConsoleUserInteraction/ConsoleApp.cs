using CW_4.Models;
using CW_4.Services;
namespace CW_4.ConsoleUserInteraction;

public class ConsoleApp
{
    private List<Pet> _pets = new List<Pet>();

    private readonly PetService _petService = new PetService();
    private readonly PetStorageService _storageService = new PetStorageService();
    private readonly ConsoleMenu _menu = new ConsoleMenu();
    private readonly PetInputReader _inputReader = new PetInputReader();
    private readonly PetTablePrinter _tablePrinter = new PetTablePrinter();

    public ConsoleApp()
    {
        SubscribeToEvents();
        _pets = _storageService.Load();
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            _menu.Show();

            Console.Write("Выберите действие: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _tablePrinter.Print(_pets);
                    break;

                case "2":
                    AddPet();
                    break;

                case "3":
                    FeedPet();
                    break;

                case "4":
                    PlayWithPet();
                    break;

                case "5":
                    HealPet();
                    break;

                case "0":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void AddPet()
    {
        try
        {
            string type = _inputReader.ReadAnimal();
            string name = _inputReader.ReadUniquePetName(_pets);
            int ageInDays = _inputReader.ReadAgeInDays();
            _petService.AddPet(_pets, name, type, ageInDays);
            SavePets();
            Console.WriteLine("Животное добавлено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private void FeedPet()
    {
        ApplyActionToPet("feed");
    }

    private void PlayWithPet()
    {
        ApplyActionToPet("play");
    }

    private void HealPet()
    {
        ApplyActionToPet("heal");
    }

    private void ApplyActionToPet(string action)
    {
        try
        {
            if (_pets.Count == 0)
            {
                Console.WriteLine("Список животных пуст.");
                return;
            }
            _tablePrinter.Print(_pets);
            Pet pet = _inputReader.ReadPetByNumberOrName(_pets);
            switch (action)
            {
                case "feed":
                    _petService.Feed(_pets, pet);
                    SavePets();
                    Console.WriteLine("Действие выполнено: кормление.");
                    break;
                case "play":
                    _petService.Play(_pets, pet);
                    SavePets();
                    Console.WriteLine("Действие выполнено: игра.");
                    break;
                case "heal":
                    _petService.Heal(_pets, pet);
                    SavePets();
                    Console.WriteLine("Действие выполнено: лечение.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    private void SavePets()
    {
        _storageService.Save(_pets);
    }

    private void SubscribeToEvents()
    {
        _petService.SatietyMaxReached += (_, e) =>
            Console.WriteLine($"Событие: {e.Pet.Name}. {e.Message}");
        _petService.MoodMaxReached += (_, e) =>
            Console.WriteLine($"Событие: {e.Pet.Name}. {e.Message}");
        _petService.HealthMaxReached += (_, e) =>
            Console.WriteLine($"Событие: {e.Pet.Name}. {e.Message}");
        _petService.RandomEventHappened += (_, e) =>
            Console.WriteLine(e.Message);
        _petService.PetDied += (_, e) =>
        {
            Console.WriteLine($"Животное {e.Pet.Name} умерло: {e.Reason}.");
            SavePets();
        };
    }
}
