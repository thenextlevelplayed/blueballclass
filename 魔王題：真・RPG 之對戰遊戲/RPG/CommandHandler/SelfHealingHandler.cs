using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class SelfHealingHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "自我治療";
    }

    protected override void Process(Role role)
    {
        var selfHealing = new SelfHealing(role);
        role.Commands.Add(new SelfHealingCommand(selfHealing));
    }
}