using RPG.Command;
using RPG.Enum;
using RPG.Status;

namespace RPG.ActionOption;

public interface IActionOption
{
    int Mp { get; set; }
    int Str { get; set; }
    State State { get; set; }
    bool PassS2 { get; set; }
    TargetCondition TargetCondition { get; set; }

    void DoAction(List<Role> roles);
}

public abstract class Skill : IActionOption
{
    // 將所有子類別需要獨立設定的屬性宣告為 abstract
    public abstract int Mp { get; set; }
    public abstract int Str { get; set; }
    public abstract State State { get; set; }
    public bool PassS2 { get; set; } = false; // 預設值可以保留
    public abstract TargetCondition TargetCondition { get; set; }

    private Role Role { get; set; } = null!;
    
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
            role.EnterState(State);
        }
    }

    protected abstract void ActionHook(List<Role> roles);
}

public class Waterball(Role role) : Skill(role)
{
    // 使用 override 關鍵字來實作所有抽象屬性

    public override int Mp { get; set; } = 50;
    public override int Str { get; set; } = 120;
    public override State State { get; set; };
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
    public State State { get; set; } = new Normal();
    public bool PassS2 { get; set; }
    public TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);
    public Role Role { get; set; }

    public void DoAction(List<Role> roles)
    {
        if (Role.CanAction())
        {
            roles[0].Hp -= Role.Str;
        }
    }
}