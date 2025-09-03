using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class FireBallHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "火球";
    }

    protected override void Process(Role role)
    {
        var fireBall = new FireBall(role);
        role.Commands.Add(new FireBallCommand(fireBall));
    }
}