using RPG.ActionOption;

namespace RPG.Command;

public class CurseCommand(Curse curse) : ICommand
{
    public IActionOption ActionOption => curse;

    public void Execute(List<Role> targets)
    {
        curse.DoAction(targets);
    }
}