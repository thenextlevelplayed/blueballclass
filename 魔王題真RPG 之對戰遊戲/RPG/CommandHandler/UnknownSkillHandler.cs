namespace RPG.CommandHandler;

public class UnknownSkillHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        // 它不處理任何請求，只負責拋出例外
        return false;
    }

    protected override void Process(Role role)
    {
        // 這裡永遠不會被呼叫，因為 CanHandle 永遠是 false。
    }
}