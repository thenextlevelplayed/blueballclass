using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class SummonHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "召喚";
    }

    protected override void Process(Role role)
    {
        var summon = new Summon(role);
        role.Commands.Add(new SummonCommand(summon));
    }
}