using RPG.ActionOption;

namespace RPG.Command;

public class PetrochemicalCommand(Petrochemical petrochemical) : ICommand
{
    public IActionOption ActionOption => petrochemical;

    public void Execute(List<Role> targets)
    {
        petrochemical.DoAction(targets);
    }
}