using RPG.ActionOption;

namespace RPG.Command;

public class SummonCommand(Summon summon) : ICommand
{
    public IActionOption ActionOption => summon;

    public void Execute(List<Role> targets)
    {
        summon.DoAction(targets);
    }
}