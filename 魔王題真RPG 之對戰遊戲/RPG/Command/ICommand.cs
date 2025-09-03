using RPG.ActionOption;

namespace RPG.Command;

public interface ICommand
{
    IActionOption ActionOption { get; }
    void Execute(List<Role> targets);
}