namespace CW_4.States;

public class PetAgeStateFactory
{
    public static IAgeState GetState(int age)
    {
        if (age <= 5)
        {
            return new YoungPetState();
        }
        if (age <= 10)
        {
            return new AdultPetState();
        }
        return new OldPetState();
    }
}