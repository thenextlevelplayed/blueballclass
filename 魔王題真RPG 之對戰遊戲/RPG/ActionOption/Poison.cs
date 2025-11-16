using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

public class Poison(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 80;
    public override int Str { get; set; } = 0;

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