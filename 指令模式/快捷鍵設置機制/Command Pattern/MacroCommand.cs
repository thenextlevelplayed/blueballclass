namespace 快捷鍵設置機制.Command_Pattern;

public class MacroCommand:ICommand
{
    private readonly List<ICommand> _commands;

    public MacroCommand(List<ICommand> commands)
    {
        _commands =  commands;
    }
    public void Execute()
    {
        _commands.ForEach(command => command.Execute());
    }

    public void Undo()
    {
        _commands.Reverse();
        _commands.ForEach(command => command.Undo());
    }
}