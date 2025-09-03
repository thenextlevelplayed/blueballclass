using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class CheerUpHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "鼓舞";
    }

    protected override void Process(Role role)
    {
        var cheerUp = new CheerUp(role);
        role.Commands.Add(new CheerUpCommand(cheerUp));
    }
}