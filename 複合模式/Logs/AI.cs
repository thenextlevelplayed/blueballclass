namespace Logs;

public class AI
{
    private Logger Logger { get; set; }
    public string Name { get; set; }

    public AI(string name)
    {
        Name = name;
        Logger = LoggerRegistry.GetLogger("app.game.ai");
    }

    public void MakeDecision()
    {
        Logger.Trace($"{Name} starts making decisions...");

        Logger.Warn($"{Name} decides to give up.");
        Logger.Error($"Something goes wrong when AI gives up.");

        Logger.Trace($"{Name} completes its decision.");
    }
}