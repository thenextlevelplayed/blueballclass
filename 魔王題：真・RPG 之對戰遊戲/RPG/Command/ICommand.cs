using RPG.ActionOption;

namespace RPG.Command;

public interface ICommand
{
    void Execute(List<Role> targets);
}

public class WaterBallCommand : ICommand
{
    private Waterball waterball = new Waterball();
    public void Execute(List<Role> targets)
    {
        waterball.DoAction(targets);
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