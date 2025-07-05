namespace MatchmakingSystem;

public class MatchmakingSystem
{
    private int SelectStrategy { get; set; }

    public MatchmakingSystem(int selectStrategy)
    {
        SelectStrategy = selectStrategy;
    }

    public void Start()
    {
        
    }

    public void Match()
    {
        switch (SelectStrategy)
        {
            case 0:
                // do something DistanceBasedStrategy()
                Console.WriteLine("im 0.");
                break;
            case 1:
                //HabitBasedStrategy
                break;
        }
    }
}