namespace 快捷鍵設置機制.Prototype;

public class Telecom
{
    private bool _on;

    public void Connect()
    {
        this._on = true;
        Console.WriteLine("ConnectTelecom");
    }

    public void Disconnect()
    {
        this._on = false;
        Console.WriteLine("DisconnectTelecom");
    }
}