namespace 快捷鍵設置機制.Prototype;

public class Macro
{
    private Keyboard _keyboard;
    public Dictionary<Char, List<int>?> SavedBindKeys = new Dictionary<Char, List<int>?>();


    public Macro( Keyboard keyboard)
    {
        _keyboard = keyboard;
        PairKey();
    }

    public void BindKey(string key, string bindValues)
    {
        Char.TryParse(key, out char charKey);
        var intList = bindValues
            .Split(' ')
            .Select(value => int.TryParse(value, out int result) ? result : (int?)null)
            .Where(result => result.HasValue)
            .Select(result => result.Value)
            .ToList();
        SavedBindKeys[charKey] = intList;
    }
    
    private void PairKey()
    {
        foreach (var bindKey in _keyboard.Keys)
        {
            SavedBindKeys[bindKey.Key] = null;
        }
    }

    public void Reset()
    {
        SavedBindKeys.Clear();
    }
}