using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

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
        var hpHandler = new HpIsBiggerThan500(this);
        var debuffHandler = new TargetStatusIsDebuff(this);
        var buffHandler = new TargetStatusIsBuff(this);
        var normalHandler = new TargetStatusIsNormal(this);
        hpHandler.SetNext(debuffHandler)
            .SetNext(buffHandler)
            .SetNext(normalHandler);

        foreach (var role in roles)
        {
            hpHandler.Handle(role);
            // if (role.Hp >= 500)
            // {
            //     Attack(role, 300);
            // }
            // else if (role.State is Poisoned || role.State is Status.Petrochemical)
            // {
            //     for (int i = 0; i < 3; i++)
            //     {
            //         Attack(role, 80);
            //     }
            //
            //     ChangeState(role);
            // }
            // else if (role.State is Status.CheerUp)
            // {
            //     Attack(role, Str);
            //     ChangeState(role);
            // }
            // else if (role.State is Normal)
            // {
            //     Attack(role, Str);
            // }
        }
    }

    public override string ToString()
    {
        return "一拳攻擊";
    }
}