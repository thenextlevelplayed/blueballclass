namespace RPG.ActionOption;

public class TargetStatusIsBuff(OnePunch skill) : AbstractOnePunchCommandHandler(skill)
{
    protected override bool CanHandle(Role role)
    {
        return role.State is Status.CheerUp;
    }

    protected override void Process(Role role)
    {
        Skill.Attack(role, Str);
        Skill.ChangeState(role);
    }
}