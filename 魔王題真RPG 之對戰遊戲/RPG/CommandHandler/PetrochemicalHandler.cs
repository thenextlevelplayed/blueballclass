using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class PetrochemicalHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "石化";
    }

    protected override void Process(Role role)
    {
        var petrochemical = new Petrochemical(role);
        role.Commands.Add(new PetrochemicalCommand(petrochemical));
    }
}