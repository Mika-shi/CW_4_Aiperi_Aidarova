using CW_4.Models;
namespace CW_4.Comparers;

public class PetLifeLevelComparer : IComparer<Pet>
{
    public int Compare(Pet? x, Pet? y)
    {
        if (x == null && y == null)
        {
            return 0;
        }
        if (x == null)
        {
            return 1;
        }
        if (y == null)
        {
            return -1;
        }
        return y.AverageLifeLevel.CompareTo(x.AverageLifeLevel);
    }
}