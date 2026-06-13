namespace CW_4.Models;

public class Pet
{
    private const int StartLevel = 30;
    public string Name { get; set; }
    public string Type { get; set; }
    public int AgeInDays { get; set; }
    public int Satiety { get; set; }
    public int Mood { get; set; }
    public int Health { get; set; }
    public double AverageLifeLevel => (Satiety + Mood + Health) / 3.0;
    public Pet()
    {
        Name = string.Empty;
        Type = string.Empty;
    }
    public Pet(string name, string type, int ageInDays)
    {
        Name = name;
        Type = type;
        AgeInDays = ageInDays;
        Satiety = StartLevel;
        Mood = StartLevel;
        Health = StartLevel;
    }
}
