namespace 快捷鍵設置機制.Prototype;

public class Controller
{
    public Controller(Keyboard keyboard, Tank tank, Telecom telecom, Macro macro)
    {
        Keyboard = keyboard;
        _tank = tank;
        _telecom = telecom;
        _macro = macro;
        PairKey();
    }

    public Keyboard Keyboard;
    private Tank _tank;
    private Telecom _telecom;
    private Macro _macro;
    private Dictionary<Char, int?> _savedBindKeys = new Dictionary<Char, int?>();

    private void PairKey()
    {
        foreach (var bindKey in Keyboard.Keys)
        {
            _savedBindKeys[bindKey.Key] = null;
        }
    }

    public void BindKey(string key, string bindValue)
    {
        Char.TryParse(key, out char charKey);
        int.TryParse(bindValue, out int intValue);
        _savedBindKeys[charKey] = intValue;
    }

    public void PressButton(string key)
    {
        Char.TryParse(key, out char charKey);
        var task = _savedBindKeys[charKey];
        if (task != null)
        {
            switch (task)
            {
                case 0:
                    _tank.MoveForward();
                    return;
                case 1:
                    _tank.MoveBackward();
                    return;
                case 2:
                    _telecom.Connect();
                    return;
                case 3:
                    _telecom.Disconnect();
                    return;
                case 4:
                    Reset();
                    _macro.Reset();
                    return;
            }
        }
        else
        {
            var macroTasks = _macro.SavedBindKeys[charKey];
            foreach (var macroTask in macroTasks)
            {
                switch (macroTask)
                {
                    case 0:
                        _tank.MoveForward();
                        break;
                    case 1:
                        _tank.MoveBackward();
                        break;
                    case 2:
                        _telecom.Connect();
                        break;
                    case 3:
                        _telecom.Disconnect();
                        break;
                    case 4:
                        Reset();
                        _macro.Reset();
                        break;
                }
            }
        }
        
        
    }

    private void Reset()
    {
        _savedBindKeys.Clear();
    }
}