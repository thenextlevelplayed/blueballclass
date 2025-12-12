namespace Logs;

public class Game
{
    private Logger Logger { get; set; } = LoggerRegistry.GetLogger("app.game");

    public void Start()
    {
        var players = new List<AI> { new AI("AI 1"), new AI("AI 2"), new AI("AI 3"), new AI("AI 4") };
        Logger.Info("The game begins.");

        // 每個 AI 玩家輪流做決策
        foreach (var ai in players)
        {
            Logger.Trace($"The player *{ai.Name}* begins his turn.");
            ai.MakeDecision();
            Logger.Trace($"The player *{ai.Name}* finishes his turn.");
        }

        Logger.Debug("Game ends.");
    }
}