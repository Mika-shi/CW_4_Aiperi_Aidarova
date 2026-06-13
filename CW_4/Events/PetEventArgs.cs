using CW_4.Models;

namespace CW_4.Events;

public class PetEventArgs : EventArgs
{
    public Pet Pet { get; }
    public string Message { get; }
    public PetEventArgs(Pet pet, string message)
    {
        Pet = pet;
        Message = message;
    }
}