namespace 快捷鍵設置機制.Command_Pattern;

public class Telecom
{
    private bool _on;

    public void Connect()
    {
        this._on = true;
        Console.WriteLine("The telecom has been turned on.");
    }

    public void Disconnect()
    {
        this._on = false;
        Console.WriteLine("The telecom has been turned off.");
    }
}