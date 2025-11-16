using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

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