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

    protected void ChangeState(Role role)
    {
        role.EnterState(GetState(role)!);
    }

    protected abstract void ActionHook(List<Role> roles);
}

public class OnePunch : Skill
{
    public OnePunch(Role role) : base(role)
    {
    }

    public override int Mp { get; set; } = 180;
    public override int Str { get; set; } = 100;

    protected override State? GetState(Role target)
    {
        return new Normal(target);
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);

    protected override void ActionHook(List<Role> roles)
    {
        foreach (var role in roles)
        {
            if (role.Hp >= 500)
            {
                role.Hp -= 300;
            }
            else if (role.State is Poisoned || role.State is Status.Petrochemical)
            {
                for (int i = 0; i < 3; i++)
                {
                    role.Hp -= 80;
                }

                ChangeState(role);
            }
            else if (role.State is Status.CheerUp)
            {
                role.Hp -= Str;
            }
            else if (role.State is Normal)
            {
                role.Hp -= Str;
            }
        }
    }
    public override string ToString()
    {
        return "一拳攻擊";
    }
}

public class Curse(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 100;
    public override int Str { get; set; } = 0;

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);

    protected override void ActionHook(List<Role> roles)
    {
        SpellcasterAndTheCursed spellcasterAndTheCursed = new SpellcasterAndTheCursed(Role, roles[0]);
        Role.Spellcaster.Add(spellcasterAndTheCursed);
        roles[0].TheCursed.Add(spellcasterAndTheCursed);
    }
    public override string ToString()
    {
        return "詛咒";
    }
}

public class CheerUp : Skill
{
    public CheerUp(Role role) : base(role)
    {
    }

    public override int Mp { get; set; } = 100;
    public override int Str { get; set; } = 0;

    protected override State? GetState(Role target)
    {
        return new Status.CheerUp(target);
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Ally, 3);

    protected override void ActionHook(List<Role> roles)
    {
        foreach (var role in roles)
        {
            ChangeState(role);
        }
    }
    public override string ToString()
    {
        return "鼓舞";
    }
}

public class SelfExplosion : Skill
{
    public SelfExplosion(Role role) : base(role)
    {
    }

    public override int Mp { get; set; } = 200;
    public override int Str { get; set; } = 150;
    public override bool PassS2 { get; set; } = true;

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.AllEnemy, -1);

    protected override void ActionHook(List<Role> roles)
    {
        foreach (var role in roles)
        {
            role.Hp -= Str;
        }

        Role.Hp = 0;
    }
    public override string ToString()
    {
        return "自爆";
    }
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

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.None, 0);

    protected override void ActionHook(List<Role> roles)
    {
        var slime = new AI("Slime", 100, 0, 50);
        Role.Troop.Roles.Add(slime);
        slime.Troop = Role.Troop;
        Role.Troop.Rpg.UpdateTroop(Role.Troop);
        SummonerAndSummon summonerAndSummon = new SummonerAndSummon(Role, slime);
        Role.Summoner.Add(summonerAndSummon);
        slime.Summoned = summonerAndSummon;
        Role.RegisterObserver(new SummonBuff(slime));
    }
    public override string ToString()
    {
        return "召喚";
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
        foreach (var role in roles)
        {
            ChangeState(role);
        }
    }
    public override string ToString()
    {
        return "下毒";
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
        foreach (var role in roles)
        {
            ChangeState(role);
        }
    }
    public override string ToString()
    {
        return "石化";
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
    public override string ToString()
    {
        return "自我治療";
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

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.AllEnemy, -1);

    protected override void ActionHook(List<Role> roles)
    {
        foreach (var role in roles)
        {
            role.Hp -= Str;
        }
    }
    public override string ToString()
    {
        return "火球";
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

    public override string ToString()
    {
        return "水球";
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

    public override string ToString()
    {
        return "普通攻擊";
    }
}