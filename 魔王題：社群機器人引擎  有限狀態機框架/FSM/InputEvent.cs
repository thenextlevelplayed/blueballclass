namespace FSM;

public class InputEvent // 移除 abstract
{
    public string Name { get; set; }
    public object Payload { get; set; } // 先用 object 擋著，Phase 2 再換成 JsonElement

    public InputEvent(string name, object payload = null)
    {
        Name = name;
        Payload = payload;
    }
}