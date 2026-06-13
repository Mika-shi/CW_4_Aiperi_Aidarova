namespace CW_4.States;

public class YoungPetState : IAgeState
{
    public int IncreaseStep => 10;
    public int DecreaseStep => 2;
}