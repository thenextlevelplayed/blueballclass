using RPG.ActionOption;

namespace RPG.Command;

public interface ICommand
{
    IActionOption ActionOption { get; }
    void Execute(List<Role> targets);
}

public class OnePunchCommand(OnePunch onePunch) : ICommand
{
    public IActionOption ActionOption => onePunch;
    public void Execute(List<Role> targets)
    {
        onePunch.DoAction(targets);
    }
}

public class CurseCommand(Curse curse) : ICommand
{
    public IActionOption ActionOption => curse;

    public void Execute(List<Role> targets)
    {
        curse.DoAction(targets);
    }
}

public class CheerUpCommand(CheerUp cheerUp) : ICommand
{
    public IActionOption ActionOption => cheerUp;

    public void Execute(List<Role> targets)
    {
        cheerUp.DoAction(targets);
    }
}

public class SelfExplosionCommand(SelfExplosion selfExplosion) : ICommand
{
    public IActionOption ActionOption => selfExplosion;

    public void Execute(List<Role> targets)
    {
        selfExplosion.DoAction(targets);
    }
}

public class SummonCommand(Summon summon) : ICommand
{
    public IActionOption ActionOption => summon;

    public void Execute(List<Role> targets)
    {
        summon.DoAction(targets);
    }
}

public class PoisonCommand(Poison poison) : ICommand
{
    public IActionOption ActionOption => poison;

    public void Execute(List<Role> targets)
    {
        poison.DoAction(targets);
    }
}

public class PetrochemicalCommand(Petrochemical petrochemical) : ICommand
{
    public IActionOption ActionOption => petrochemical;

    public void Execute(List<Role> targets)
    {
        petrochemical.DoAction(targets);
    }
}

public class SelfHealingCommand(SelfHealing selfHealing) : ICommand
{
    public IActionOption ActionOption => selfHealing;

    public void Execute(List<Role> targets)
    {
        selfHealing.DoAction(targets);
    }
}

public class FireBallCommand(FireBall fireBall) : ICommand
{
    public IActionOption ActionOption => fireBall;

    public void Execute(List<Role> targets)
    {
        fireBall.DoAction(targets);
    }
}

public class WaterBallCommand(WaterBall waterBall) : ICommand
{
    public IActionOption ActionOption => waterBall;

    public void Execute(List<Role> targets)
    {
        waterBall.DoAction(targets);
    }
}

public class BasicAttackCommand(BasicAttack basicAttack) : ICommand
{
    public IActionOption ActionOption => basicAttack;

    public void Execute(List<Role> targets)
    {
        basicAttack.DoAction(targets);
    }
}