using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

public class SelfHealing(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 50;
    public override int Str { get; set; } = 0;
    private int RecoveryHp { get; set; } = 150;

    public override bool PassS2 { get; set; } = true;

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