using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

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
            Attack(role,Str);
        }
    }
    public override string ToString()
    {
        return "火球";
    }
}