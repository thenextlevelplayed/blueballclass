using RPG.ActionOption;

namespace RPG.Command;

public class OnePunchCommand(OnePunch onePunch) : ICommand
{
    public IActionOption ActionOption => onePunch;
    public void Execute(List<Role> targets)
    {
        onePunch.DoAction(targets);
    }
}