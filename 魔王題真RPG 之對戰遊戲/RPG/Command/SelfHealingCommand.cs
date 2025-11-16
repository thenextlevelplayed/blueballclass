using RPG.ActionOption;

namespace RPG.Command;

public class SelfHealingCommand(SelfHealing selfHealing) : ICommand
{
    public IActionOption ActionOption => selfHealing;

    public void Execute(List<Role> targets)
    {
        selfHealing.DoAction(targets);
    }
}