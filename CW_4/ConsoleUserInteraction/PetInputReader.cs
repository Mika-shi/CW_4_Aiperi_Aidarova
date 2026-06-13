using CW_4.Models;
namespace CW_4.ConsoleUserInteraction;

public class PetInputReader
{
    public string ReadRequiredString(string message, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            string? value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"Ошибка: {errorMessage}");
                continue;
            }
            value = value.Trim();
            if (value.Length < 2)
            {
                Console.WriteLine("Ошибка: значение должно содержать минимум 2 символа.");
                continue;
            }
            if (value.Length > 30)
            {
                Console.WriteLine("Ошибка: значение не должно быть длиннее 30 символов.");
                continue;
            }
            if (!IsValidText(value))
            {
                Console.WriteLine("Ошибка: можно использовать только буквы, пробелы и дефис.");
                continue;
            }
            return value;
        }
    }

    public string ReadUniquePetName(List<Pet> pets)
    {
        while (true)
        {
            string name = ReadRequiredString(
                "Введите имя животного: ",
                "Имя животного не может быть пустым."
            );
            bool nameExists = pets.Any(
                pet => pet.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );
            if (nameExists)
            {
                Console.WriteLine("Ошибка: животное с таким именем уже существует.");
                continue;
            }
            return name;
        }
    }

    public int ReadAgeInDays()
    {
        while (true)
        {
            Console.Write("Введите возраст животного, например: 1 год 3 месяца 6 дней: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: возраст не может быть пустым.");
                continue;
            }
            string[] parts = input
                .Trim()
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length % 2 != 0)
            {
                Console.WriteLine("Ошибка: возраст нужно вводить парами: число и единица измерения.");
                continue;
            }
            int totalDays = 0;
            bool hasError = false;
            for (int i = 0; i < parts.Length; i += 2)
            {
                if (!int.TryParse(parts[i], out int value))
                {
                    Console.WriteLine("Ошибка: перед единицей измерения должно быть число.");
                    hasError = true;
                    break;
                }
                if (value < 0)
                {
                    Console.WriteLine("Ошибка: возраст не может быть отрицательным.");
                    hasError = true;
                    break;
                }
                string unit = parts[i + 1];
                if (unit == "год" || unit == "года" || unit == "лет")
                {
                    totalDays += value * 365;
                }
                else if (unit == "месяц" || unit == "месяца" || unit == "месяцев")
                {
                    totalDays += value * 30;
                }
                else if (unit == "день" || unit == "дня" || unit == "дней")
                {
                    totalDays += value;
                }
                else
                {
                    Console.WriteLine("Ошибка: используйте только годы, месяцы или дни.");
                    hasError = true;
                    break;
                }
            }
            if (hasError)
            {
                continue;
            }
            return totalDays;
        }
    }
    public Pet ReadPetByNumberOrName(List<Pet> pets)
    {
        while (true)
        {
            Console.Write("Введите номер или имя животного: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: нужно ввести номер или имя животного.");
                continue;
            }
            input = input.Trim();
            if (int.TryParse(input, out int number))
            {
                if (number < 1 || number > pets.Count)
                {
                    Console.WriteLine("Ошибка: животного с таким номером нет.");
                    continue;
                }
                return pets[number - 1];
            }
            Pet? pet = pets.FirstOrDefault(
                pet => pet.Name.Equals(input, StringComparison.OrdinalIgnoreCase)
            );
            if (pet == null)
            {
                Console.WriteLine("Ошибка: животного с таким именем нет.");
                continue;
            }
            return pet;
        }
    }
    private bool IsValidText(string value)
    {
        foreach (char symbol in value)
        {
            if (!char.IsLetter(symbol) && symbol != ' ' && symbol != '-')
            {
                return false;
            }
        }
        return true;
    }
    public string ReadAnimal()
    {
        while (true)
        {
            Console.Write("Введите животное (без уменьшительно-ласкательной формы. например, кот): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: Название животного не может быть пустым.");
                continue;
            }
            string animal = input.Trim().ToLower();
            if (!_allowedAnimals.Contains(animal))
            {
                Console.WriteLine("Ошибка: Такого животного нет в списке. Например, пишите \"кот\", а не \"котик\".");
                continue;
            }
            return animal;
        }
    }
    private readonly List<string> _allowedAnimals = new List<string>
    {
        "кот",
        "кошка",
        "собака",
        "попугай",
        "хомяк",
        "морская свинка",
        "кролик",
        "черепаха",
        "рыбка",
        "крыса",
        "мышь",
        "шиншилла",
        "еж",
        "змея",
        "ящерица",
        "игуана",
        "хамелеон",
        "улитка",
        "паук",
        "фретка",
        "хорек",
        "мини-пиг",
        "поросенок",
        "лошадь",
        "пони",
        "осел",
        "коза",
        "овца",
        "корова",
        "бык",
        "курица",
        "петух",
        "утка",
        "гусь",
        "индюк",
        "голубь",
        "канарейка",
        "ворон",
        "сова",
        "лиса",
        "енот",
        "сурикат",
        "обезьяна",
        "верблюд",
        "лама",
        "альпака",
        "жираф",
        "слон",
        "тигр",
        "лев",
        "пантера",
        "леопард",
        "гепард",
        "волк",
        "медведь",
        "панда",
        "кенгуру",
        "коала",
        "олень",
        "лось",
        "бегемот",
        "носорог",
        "зебра",
        "крокодил",
        "аллигатор",
        "дельфин",
        "тюлень",
        "пингвин",
        "выдра"
    };
}
