using RPG.ActionOption;

namespace RPG.Command;

public class CheerUpCommand(CheerUp cheerUp) : ICommand
{
    public IActionOption ActionOption => cheerUp;

    public void Execute(List<Role> targets)
    {
        cheerUp.DoAction(targets);
    }
}