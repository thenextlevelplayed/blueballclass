using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

public class Petrochemical(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 100;
    public override int Str { get; set; } = 0;

    protected override State? GetState(Role target)
    {
        return new Status.Petrochemical(target);
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
        return "石化";
    }
}