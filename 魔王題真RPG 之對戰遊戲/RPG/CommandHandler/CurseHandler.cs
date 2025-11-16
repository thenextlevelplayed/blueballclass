using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class CurseHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "詛咒";
    }

    protected override void Process(Role role)
    {
        var curse = new Curse(role);
        role.Commands.Add(new CurseCommand(curse));
    }
}