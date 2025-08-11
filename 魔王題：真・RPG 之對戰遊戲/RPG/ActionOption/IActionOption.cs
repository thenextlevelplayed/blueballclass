using RPG.Command;

namespace RPG.ActionOption;

public interface IActionOption
{
    int Mp { get; set; }
    int Str { get; set; }
    bool PassS2 { get; set; }
    TargetCondition TargetCondition { get; set; }
    void DoAction(List<Role> roles);
    Role Role { get; set; }
    void Attack(Role role, int str);
}