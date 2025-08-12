using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class OnePunchHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "一拳攻擊";
    }

    protected override void Process(Role role)
    {
        var onePunchAttack = new OnePunch(role);
        role.Commands.Add(new OnePunchCommand(onePunchAttack));
    }
}