using RPG.ActionOption;

namespace RPG.Command;

public class WaterBallCommand(WaterBall waterBall) : ICommand
{
    public IActionOption ActionOption => waterBall;

    public void Execute(List<Role> targets)
    {
        waterBall.DoAction(targets);
    }
}