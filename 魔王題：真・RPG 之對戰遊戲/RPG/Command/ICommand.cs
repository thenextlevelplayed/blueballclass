using RPG.ActionOption;

namespace RPG.Command;

public interface ICommand
{
    void Execute(List<Role> targets);
}

public class WaterBallCommand : ICommand
{
    private WaterBall _waterBall;

    public WaterBallCommand(WaterBall waterBall)
    {
        this._waterBall = waterBall;
    }
    public void Execute(List<Role> targets)
    {
        _waterBall.DoAction(targets);
    }
}

public class BasicAttackCommand : ICommand
{
    private BasicAttack basicAttack = new BasicAttack();
    public void Execute(List<Role> targets)
    {
        basicAttack.DoAction(targets);
    }
}