namespace CW_4.Services;

public class RandomEventResult
{
    public bool Happened { get; set; }
    public string Message { get; set; }
    public RandomEventResult(bool happened, string message)
    {
        Happened = happened;
        Message = message;
    }
}