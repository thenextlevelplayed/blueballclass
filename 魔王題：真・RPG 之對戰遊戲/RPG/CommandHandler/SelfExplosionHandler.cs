using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class SelfExplosionHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "自爆";
    }

    protected override void Process(Role role)
    {
        var selfExplosion = new SelfExplosion(role);
        role.Commands.Add(new SelfExplosionCommand(selfExplosion));
    }
}