using CW_4.States;
using CW_4.Models;
namespace CW_4.Services;

public class RandomEventService
{
    private readonly Random _random = new Random();
    public RandomEventResult ApplyFeedEvent(Pet pet, IAgeState state)
    {
        int eventNumber = _random.Next(1, 101);
        if (eventNumber <= 15)
        {
            pet.Mood -= 20;
            pet.Health -= 30;
            return new RandomEventResult(true, "Случайное событие: животное отравилось едой.");
        }
        if (eventNumber <= 30)
        {
            pet.Satiety += state.IncreaseStep + 10;
            pet.Mood += state.IncreaseStep + 10;
            return new RandomEventResult(true, "Случайное событие: животному попалась любимая еда.");
        }
        if (eventNumber <= 45)
        {
            pet.Satiety += state.IncreaseStep + 15;
            pet.Mood += 5;
            pet.Health -= 15;
            return new RandomEventResult(true, "Случайное событие: животное переело.");
        }
        return new RandomEventResult(false, string.Empty);
    }

    public RandomEventResult ApplyPlayEvent(Pet pet, IAgeState state)
    {
        int eventNumber = _random.Next(1, 101);
        if (eventNumber <= 15)
        {
            pet.Satiety -= state.DecreaseStep;
            pet.Mood -= 15;
            pet.Health -= 25;
            return new RandomEventResult(true, "Случайное событие: животное травмировалось во время игры.");
        }
        if (eventNumber <= 30)
        {
            pet.Satiety -= state.DecreaseStep;
            pet.Mood += state.IncreaseStep + 15;
            pet.Health += 5;
            return new RandomEventResult(true, "Случайное событие: игра прошла особенно удачно.");
        }
        if (eventNumber <= 45)
        {
            pet.Satiety -= state.DecreaseStep;
            pet.Health -= 10;
            return new RandomEventResult(true, "Случайное событие: животное сильно устало.");
        }
        return new RandomEventResult(false, string.Empty);
    }

    public RandomEventResult ApplyHealEvent(Pet pet, IAgeState state)
    {
        int eventNumber = _random.Next(1, 101);
        if (eventNumber <= 15)
        {
            pet.Mood -= 20;
            pet.Health -= 15;
            return new RandomEventResult(true, "Случайное событие: возникло осложнение после лечения.");
        }
        if (eventNumber <= 30)
        {
            pet.Health += state.IncreaseStep + 15;
            return new RandomEventResult(true, "Случайное событие: лечение прошло идеально.");
        }
        if (eventNumber <= 45)
        {
            pet.Mood -= 25;
            pet.Health += state.IncreaseStep;
            return new RandomEventResult(true, "Случайное событие: животное испугалось врача.");
        }
        return new RandomEventResult(false, string.Empty);
    }
}