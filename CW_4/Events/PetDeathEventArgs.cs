using CW_4.Models;
namespace CW_4.Events;

public class PetDeathEventArgs : EventArgs
{
    public Pet Pet { get; }
    public string Reason { get; }
    public PetDeathEventArgs(Pet pet, string reason)
    {
        Pet = pet;
        Reason = reason;
    }
}