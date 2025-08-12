using RPG.Status;

namespace RPG.ActionOption;

public class TargetStatusIsDebuff(OnePunch skill) : AbstractOnePunchCommandHandler(skill)
{
    protected override bool CanHandle(Role role)
    {
        return role.State is Poisoned || role.State is Status.Petrochemical;
    }

    protected override void Process(Role role)
    {
        for (int i = 0; i < 3; i++)
        {
            Skill.Attack(role, Str);
        }

        Skill.ChangeState(role);
    }

    public override int Str => 80;
}