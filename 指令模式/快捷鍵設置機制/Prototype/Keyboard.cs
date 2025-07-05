namespace 快捷鍵設置機制.Prototype;

public class Keyboard
{
    public Dictionary<char, bool> Keys { get; set; } = new Dictionary<char, bool>();

    public Keyboard()
    {
        Keys = CreateKeys();
    }

    private Dictionary<char, bool> CreateKeys()
    {
        var keys = new Dictionary<char, bool>();
        for (char c = 'a'; c <= 'z'; c++)
        {
            keys[c] = false;
        }

        return keys;
    }

    private void Press(char c)
    {
        Keys[c] = true;
    }
}