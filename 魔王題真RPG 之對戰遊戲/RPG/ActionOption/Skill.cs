using RPG.Status;

namespace RPG.ActionOption;

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
            LogAction(roles);
            ActionHook(roles);
        }
    }

    public void ChangeState(Role role)
    {
        role.EnterState(GetState(role)!);
    }

    protected abstract void ActionHook(List<Role> roles);
    
    protected virtual void LogAction(List<Role> roles)
    {
        string log = string.Join(", ", roles.Select(r => $"{r.Troop}{r.Name}"));
        Console.WriteLine($"{Role.Troop}{Role.Name} 對 {log} 使用了 {ToString()}。");
    }
    
    public void Attack(Role role,int str)
    {
        role.Hp -= str;
        Console.WriteLine($"{Role.Troop}{Role.Name} 對 {role.Troop}{role.Name} 造成 {str} 點傷害。");
    }
}