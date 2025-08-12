using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class PoisonHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "下毒";
    }

    protected override void Process(Role role)
    {
        var poison = new Poison(role);
        role.Commands.Add(new PoisonCommand(poison));
    }
}