namespace 快捷鍵設置機制.Command_Pattern;

public class Keyboard
{
    public Dictionary<char, ICommand?> Keys { get; set; }

    public Keyboard()
    {
        Keys = CreateKeys();
    }

    private Dictionary<char, ICommand?> CreateKeys()
    {
        var keys = new Dictionary<char, ICommand?>();
        for (char c = 'a'; c <= 'z'; c++)
        {
            keys[c] = null;
        }

        return keys;
    }

    public void Reset()
    {
        foreach (var key in Keys.Keys.ToList())
        {
            Keys[key] = null;
        }
    }
}