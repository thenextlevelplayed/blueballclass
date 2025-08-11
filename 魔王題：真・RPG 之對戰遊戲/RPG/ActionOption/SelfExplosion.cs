using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

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

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.All, -1);

    protected override void ActionHook(List<Role> roles)
    {
        foreach (var role in roles)
        {
            Attack(role, Str);
        }

        Role.Hp = 0;
    }
    public override string ToString()
    {
        return "自爆";
    }
}