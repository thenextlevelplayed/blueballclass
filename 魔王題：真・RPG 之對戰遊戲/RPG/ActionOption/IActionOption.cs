using RPG.Command;
using RPG.Enum;
using RPG.Observer;
using RPG.Status;

namespace RPG.ActionOption;

public interface IActionOption
{
    int Mp { get; set; }
    int Str { get; set; }
    bool PassS2 { get; set; }
    TargetCondition TargetCondition { get; set; }
    void DoAction(List<Role> roles);
    Role Role { get; set; }
}

public abstract class Skill : IActionOption
{
    // 將所有子類別需要獨立設定的屬性宣告為 abstract
    public abstract int Mp { get; set; }
    public abstract int Str { get; set; }

    public virtual bool PassS2 { get; set; } = false; // 預設值可以保留
    protected abstract State? GetState(Role target);

    public abstract TargetCondition TargetCondition { get; set; }
    public Role Role { get; set; }

    protected Skill(Role role)
    {
        Role = role;
    }

    public void DoAction(List<Role> roles)
    {
        if (Role.CanAction())
        {
            ActionHook(roles);
        }
    }

    protected void ChangeState(List<Role> roles)
    {
        foreach (var role in roles)
        {
            role.EnterState(GetState(role)!);
        }
    }

    protected abstract void ActionHook(List<Role> roles);
}

public class Summon : Skill
{
    public Summon(Role role) : base(role)
    {
    }

    public override int Mp { get; set; } = 150;
    public override int Str { get; set; } = 0;
    public override bool PassS2 { get; set; } = true;

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; }
    protected override void ActionHook(List<Role> roles)
    {
        var slime = new AI("Slime", 100, 0, 50);
        Role.Troop.Roles.Add(slime);
        slime.Troop = Role.Troop;
        Role.Troop.Rpg.UpdateTroop(Role.Troop);
        Role.SummonerAndSummon.Summoner = Role;
        Role.SummonerAndSummon.Summoned.Add(slime);
        Role.RegisterObserver(new SummonBuff(slime));
    }
}

public class Poison(Role role) : Skill(role)
{
    public override int Mp { get; set; }
    public override int Str { get; set; }
    protected override State? GetState(Role target)
    {
        return new Poisoned(target);
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);
    protected override void ActionHook(List<Role> roles)
    {
        ChangeState(roles);
    }
}

public class Petrochemical(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 100;
    public override int Str { get; set; } = 0;
    protected override State? GetState(Role target)
    {
        return new Status.Petrochemical(target);
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Ally, 1);
    protected override void ActionHook(List<Role> roles)
    {
        ChangeState(roles);
    }
}

public class SelfHealing(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 50;
    public override int Str { get; set; } = 0;
    private int RecoveryHp { get; set; } = 150;
    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Self, 1);
    protected override void ActionHook(List<Role> roles)
    {
        Role.Hp += RecoveryHp;
    }
}

public class FireBall(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 50;
    public override int Str { get; set; } = 50;
    public override bool PassS2 { get; set; } = true; // 預設值可以保留

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.All, -1);

    protected override void ActionHook(List<Role> roles)
    {
        foreach (var role in roles)
        {
            role.Hp -= Str;
        }
    }
}

public class WaterBall(Role role) : Skill(role)
{
    // 使用 override 關鍵字來實作所有抽象屬性

    public override int Mp { get; set; } = 50;
    public override int Str { get; set; } = 120;

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);

    protected override void ActionHook(List<Role> roles)
    {
        roles[0].Hp -= Str;
    }
}

public class BasicAttack : IActionOption
{
    public int Mp { get; set; }
    public int Str { get; set; }
    public bool PassS2 { get; set; }
    public TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);
    public Role Role { get; set; }

    public void DoAction(List<Role> roles)
    {
        roles[0].Hp -= Role.Str;
    }
}