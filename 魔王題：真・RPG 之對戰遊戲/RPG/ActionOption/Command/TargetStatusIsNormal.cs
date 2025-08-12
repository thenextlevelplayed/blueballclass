using RPG.Status;

namespace RPG.ActionOption;

public class TargetStatusIsNormal(OnePunch skill) : AbstractOnePunchCommandHandler(skill)
{
    protected override bool CanHandle(Role role)
    {
        return role.State is Normal;
    }

    protected override void Process(Role role)
    {
        Skill.Attack(role, Str);
    }
}