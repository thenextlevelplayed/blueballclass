namespace RPG.ActionOption;

public class HpIsBiggerThan500(OnePunch skill) : AbstractOnePunchCommandHandler(skill)
{
    protected override bool CanHandle(Role role)
    {
        return role.Hp >= 500;
    }

    protected override void Process(Role role)
    {
        Skill.Attack(role, Str);
    }

    public override int Str => 300;
}