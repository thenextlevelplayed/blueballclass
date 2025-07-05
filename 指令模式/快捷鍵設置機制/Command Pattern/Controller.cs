namespace 快捷鍵設置機制.Command_Pattern;

public class Controller
{
    public Controller(Keyboard keyboard, Tank tank, Telecom telecom)
    {
        _keyboard = keyboard;
    }

    private readonly Keyboard _keyboard;
    private readonly Stack<ICommand> _s1 = new Stack<ICommand>();
    private readonly Stack<ICommand> _s2 = new Stack<ICommand>();


    public void SetCommand(string key, ICommand command)
    {
        char charKey = key.ToLower()[0];
        if (_keyboard.Keys.ContainsKey(charKey))
        {
            _keyboard.Keys[charKey] = command;
        }
        else
        {
            Console.WriteLine($"Key '{key}' does not exist on the keyboard.");
        }
    }

    public void PressButton(string key)
    {
        char charKey = key.ToLower()[0];
        if (_keyboard.Keys.ContainsKey(charKey))
        {
            var task = _keyboard.Keys[charKey];
            if (task==null)
            {
                Console.WriteLine($"No command bound to key '{key}'.");
                return;
            }

            task.Execute();
            _s1.Push(task);
            _s2.Clear();
        }
        else
        {
            Console.WriteLine($"Key '{key}' does not exist on the keyboard.");
        }
    }

    public void Redo()
    {
        if (_s2.Any())
        {
            ICommand nextCommand = _s2.Pop();
            nextCommand.Execute();
            _s1.Push(nextCommand);
        }
    }

    public void Undo()
    {
        if (_s1.Any())
        {
            // undo時，把S1最上層pop出來，並執行此指令undo操作，完成此操作後，push到S2中。
            ICommand previousCommand = _s1.Pop();
            previousCommand.Undo();
            _s2.Push(previousCommand);
        }
    }
}