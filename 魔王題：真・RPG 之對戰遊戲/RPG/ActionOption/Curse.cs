using RPG.Enum;
using RPG.Observer;
using RPG.Status;

namespace RPG.ActionOption;

public class Curse(Role role) : Skill(role)
{
    public override int Mp { get; set; } = 100;
    public override int Str { get; set; } = 0;

    protected override State? GetState(Role target)
    {
        return null;
    }

    public override TargetCondition TargetCondition { get; set; } = new TargetCondition(TargetRelation.Enemy, 1);

    protected override void ActionHook(List<Role> roles)
    {
        SpellcasterAndTheCursed spellcasterAndTheCursed = new SpellcasterAndTheCursed(Role, roles[0]);
        Role.Spellcaster.Add(spellcasterAndTheCursed);
        roles[0].TheCursed.Add(spellcasterAndTheCursed);
        if (!roles[0].Observers.OfType<CurseBuff>().Any())
        {
            // 如果沒有，才註冊一個新的
            roles[0].RegisterObserver(new CurseBuff(roles[0]));
        }
    }
    public override string ToString()
    {
        return "詛咒";
    }
}