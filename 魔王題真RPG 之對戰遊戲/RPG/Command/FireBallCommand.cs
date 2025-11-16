using RPG.ActionOption;

namespace RPG.Command;

public class FireBallCommand(FireBall fireBall) : ICommand
{
    public IActionOption ActionOption => fireBall;

    public void Execute(List<Role> targets)
    {
        fireBall.DoAction(targets);
    }
}