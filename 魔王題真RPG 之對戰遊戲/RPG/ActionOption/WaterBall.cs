using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

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
        Attack(roles[0],Str);
        
    }

    public override string ToString()
    {
        return "水球";
    }
}