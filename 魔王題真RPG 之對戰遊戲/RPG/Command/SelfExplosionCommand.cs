using RPG.ActionOption;

namespace RPG.Command;

public class SelfExplosionCommand(SelfExplosion selfExplosion) : ICommand
{
    public IActionOption ActionOption => selfExplosion;

    public void Execute(List<Role> targets)
    {
        selfExplosion.DoAction(targets);
    }
}