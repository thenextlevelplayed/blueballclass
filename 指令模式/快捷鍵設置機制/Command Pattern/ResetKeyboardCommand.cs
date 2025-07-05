using System.Windows.Input;

namespace 快捷鍵設置機制.Command_Pattern;

public class ResetKeyboardCommand : ICommand
{
    private Keyboard _keyboard;
    private Dictionary<char, ICommand> _savedState;

    public ResetKeyboardCommand(Keyboard keyboard)
    {
        _keyboard = keyboard;
    }

    public void Execute()
    {
        _savedState = new Dictionary<char, ICommand>();
        foreach (var kvp in _keyboard.Keys)
        {
            _savedState[kvp.Key] = kvp.Value;
        }

        _keyboard.Reset();
        Console.WriteLine("All shortcuts have been reset.");
    }

    public void Undo()
    {
        if (_savedState != null)
        {
            _keyboard.Keys.Clear();

            foreach (var kvp in _savedState)
            {
                _keyboard.Keys[kvp.Key] = kvp.Value;
            }

            Console.WriteLine("Undo Reset: Previous shortcuts restored.");
        }
        else
        {
            Console.WriteLine("Undo Reset: No previous state saved.");
        }
    }
}