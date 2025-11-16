using RPG.ActionOption;

namespace RPG.Command;

public class PoisonCommand(Poison poison) : ICommand
{
    public IActionOption ActionOption => poison;

    public void Execute(List<Role> targets)
    {
        poison.DoAction(targets);
    }
}