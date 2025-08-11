using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public interface ICommandHandler
{
    ICommandHandler SetNext(ICommandHandler handler);
    void Handle(string skillName, Role role);
}

public abstract class AbstractCommandHandler : ICommandHandler
{
    private ICommandHandler NextHandler;

    public ICommandHandler SetNext(ICommandHandler handler)
    {
        this.NextHandler = handler;
        return handler;
    }

    public void Handle(string skillName, Role role)
    {
        // 模板方法的骨架：
        // 1. 檢查自己是否能處理
        if (CanHandle(skillName))
        {
            // 2. 如果可以，執行處理動作
            Process(role);
        }
        // 3. 如果不能，將請求傳遞給下一個處理者
        else if (this.NextHandler != null)
        {
            this.NextHandler.Handle(skillName, role);
        }
    }

    // 抽象方法：讓子類別實作「檢查是否能處理」的邏輯
    protected abstract bool CanHandle(string skillName);

    // 抽象方法：讓子類別實作「處理請求」的細節
    protected abstract void Process(Role role);
}

// 具體處理者
public class WaterBallHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "水球";
    }

    protected override void Process(Role role)
    {
        var waterBall = new WaterBall(role);
        role.Commands.Add(new WaterBallCommand(waterBall));
    }
}

// 具體處理者
public class FireBallHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "火球";
    }

    protected override void Process(Role role)
    {
        var fireBall = new FireBall(role);
        role.Commands.Add(new FireBallCommand(fireBall));
    }
}

public class SelfHealingHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "自我治療";
    }

    protected override void Process(Role role)
    {
        var selfHealing = new SelfHealing(role);
        role.Commands.Add(new SelfHealingCommand(selfHealing));
    }
}

public class PetrochemicalHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "石化";
    }

    protected override void Process(Role role)
    {
        var petrochemical = new Petrochemical(role);
        role.Commands.Add(new PetrochemicalCommand(petrochemical));
    }
}

public class PoisonHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "下毒";
    }

    protected override void Process(Role role)
    {
        var poison = new Poison(role);
        role.Commands.Add(new PoisonCommand(poison));
    }
}

public class SummonHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "召喚";
    }

    protected override void Process(Role role)
    {
        var summon = new Summon(role);
        role.Commands.Add(new SummonCommand(summon));
    }
}

public class SelfExplosionHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "自爆";
    }

    protected override void Process(Role role)
    {
        var selfExplosion = new SelfExplosion(role);
        role.Commands.Add(new SelfExplosionCommand(selfExplosion));
    }
}

public class CheerUpHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "鼓舞";
    }

    protected override void Process(Role role)
    {
        var cheerUp = new CheerUp(role);
        role.Commands.Add(new CheerUpCommand(cheerUp));
    }
}

public class CurseHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "詛咒";
    }

    protected override void Process(Role role)
    {
        var curse = new Curse(role);
        role.Commands.Add(new CurseCommand(curse));
    }
}

public class OnePunchHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "一拳攻擊";
    }

    protected override void Process(Role role)
    {
        var onePunchAttack = new OnePunch(role);
        role.Commands.Add(new OnePunchCommand(onePunchAttack));
    }
}

// 處理未知技能的處理者，放在責任鏈的末端
public class UnknownSkillHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        // 它不處理任何請求，只負責拋出例外
        return false;
    }

    protected override void Process(Role role)
    {
        // 這裡永遠不會被呼叫，因為 CanHandle 永遠是 false。
    }
}