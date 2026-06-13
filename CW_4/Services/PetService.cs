using CW_4.Events;
using CW_4.Models;
using CW_4.States;
using CW_4.Helpers;

namespace CW_4.Services;

public class PetService
{
    private const int MinLevel = 0;
    private const int MaxLevel = 100;
    private readonly RandomEventService _randomEventService = new RandomEventService();
    public event EventHandler<PetEventArgs>? SatietyMaxReached;
    public event EventHandler<PetEventArgs>? MoodMaxReached;
    public event EventHandler<PetEventArgs>? HealthMaxReached;
    public event EventHandler<PetDeathEventArgs>? PetDied;
    public event EventHandler<PetEventArgs>? RandomEventHappened;
    public void AddPet(List<Pet> pets, string name, string type, int ageInDays)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя животного не может быть пустым.");
        }
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException("Тип животного не может быть пустым.");
        }
        if (ageInDays < 0)
        {
            throw new ArgumentException("Возраст животного не может быть отрицательным.");
        }
        if (pets.Any(pet => pet.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("Животное с таким именем уже существует.");
        }
        pets.Add(new Pet(name, type, ageInDays));
    }

    public void Feed(List<Pet> pets, Pet pet)
    {
        IAgeState state = PetAgeStateFactory.GetState(AgeHelper.GetFullYears(pet.AgeInDays));
        RandomEventResult randomEvent = _randomEventService.ApplyFeedEvent(pet, state);
        if (randomEvent.Happened)
        {
            RandomEventHappened?.Invoke(
                this,
                new PetEventArgs(pet, randomEvent.Message)
            );
            ProcessPetState(pets, pet);
            return;
        }
        pet.Satiety += state.IncreaseStep;
        pet.Mood += state.IncreaseStep;
        pet.Health -= state.DecreaseStep;
        ProcessPetState(pets, pet);
    }

    public void Play(List<Pet> pets, Pet pet)
    {
        IAgeState state = PetAgeStateFactory.GetState(AgeHelper.GetFullYears(pet.AgeInDays));
        RandomEventResult randomEvent = _randomEventService.ApplyPlayEvent(pet, state);
        if (randomEvent.Happened)
        {
            RandomEventHappened?.Invoke(
                this,
                new PetEventArgs(pet, randomEvent.Message)
            );
            ProcessPetState(pets, pet);
            return;
        }
        pet.Satiety -= state.DecreaseStep;
        pet.Mood += state.IncreaseStep;
        pet.Health -= state.DecreaseStep;
        ProcessPetState(pets, pet);
    }

    public void Heal(List<Pet> pets, Pet pet)
    {
        IAgeState state = PetAgeStateFactory.GetState(AgeHelper.GetFullYears(pet.AgeInDays));
        RandomEventResult randomEvent = _randomEventService.ApplyHealEvent(pet, state);
        if (randomEvent.Happened)
        {
            RandomEventHappened?.Invoke(
                this,
                new PetEventArgs(pet, randomEvent.Message)
            );
            ProcessPetState(pets, pet);
            return;
        }
        pet.Mood -= state.DecreaseStep;
        pet.Health += state.IncreaseStep;
        ProcessPetState(pets, pet);
    }

    private void ProcessPetState(List<Pet> pets, Pet pet)
    {
        NormalizeLevels(pet);
        CheckMaxLevels(pet);
        CheckDeath(pets, pet);
    }

    private void CheckMaxLevels(Pet pet)
    {
        if (pet.Satiety == MaxLevel)
        {
            SatietyMaxReached?.Invoke(
                this,
                new PetEventArgs(pet, "Сытость достигла максимального значения.")
            );
        }

        if (pet.Mood == MaxLevel)
        {
            MoodMaxReached?.Invoke(
                this,
                new PetEventArgs(pet, "Настроение достигло максимального значения.")
            );
        }

        if (pet.Health == MaxLevel)
        {
            HealthMaxReached?.Invoke(
                this,
                new PetEventArgs(pet, "Здоровье достигло максимального значения.")
            );
        }
    }

    private void CheckDeath(List<Pet> pets, Pet pet)
    {
        if (pet.Satiety <= MinLevel)
        {
            RemoveDeadPet(pets, pet, "сытость упала до нуля");
            return;
        }
        if (pet.Mood <= MinLevel)
        {
            RemoveDeadPet(pets, pet, "настроение упало до нуля");
            return;
        }
        if (pet.Health <= MinLevel)
        {
            RemoveDeadPet(pets, pet, "здоровье упало до нуля");
        }
    }

    private void RemoveDeadPet(List<Pet> pets, Pet pet, string reason)
    {
        pets.Remove(pet);
        PetDied?.Invoke(
            this,
            new PetDeathEventArgs(pet, reason)
        );
    }

    private void NormalizeLevels(Pet pet)
    {
        pet.Satiety = NormalizeLevel(pet.Satiety);
        pet.Mood = NormalizeLevel(pet.Mood);
        pet.Health = NormalizeLevel(pet.Health);
    }

    private int NormalizeLevel(int level)
    {
        if (level < MinLevel)
        {
            return MinLevel;
        }
        if (level > MaxLevel)
        {
            return MaxLevel;
        }
        return level;
    }
}
