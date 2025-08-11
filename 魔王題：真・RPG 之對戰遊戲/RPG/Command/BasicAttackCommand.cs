using RPG.ActionOption;

namespace RPG.Command;

public class BasicAttackCommand(BasicAttack basicAttack) : ICommand
{
    public IActionOption ActionOption => basicAttack;

    public void Execute(List<Role> targets)
    {
        basicAttack.DoAction(targets);
    }
}