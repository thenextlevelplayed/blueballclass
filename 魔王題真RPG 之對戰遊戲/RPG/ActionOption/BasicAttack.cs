using RPG.Enum;

namespace RPG.ActionOption;

public class BasicAttack : IActionOption
{
    public int Mp { get; set; }
    public int Str { get; set; }
    public bool PassS2 { get; set; } = false;
    public TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);
    public Role Role { get; set; }

    public BasicAttack(Role role)
    {
        Role = role;
        Str = Role.Str;
    }
    public void Attack(Role role, int str)
    {
        Console.WriteLine($"{Role.Troop}{Role.Name} 攻擊 {role.Troop}{role.Name}。");
        Console.WriteLine($"{Role.Troop}{Role.Name} 對 {role.Troop}{role.Name} 造成 {str} 點傷害。");
        role.Hp -= str;
    }

    public void DoAction(List<Role> roles)
    {
        Attack(roles[0],Str);
    }

    public override string ToString()
    {
        return "普通攻擊";
    }
}