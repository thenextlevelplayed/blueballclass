namespace 快捷鍵設置機制.Command_Pattern;

public class ConnectTelecom:ICommand
{
    private readonly Telecom _telecom;

    public ConnectTelecom(Telecom telecom)
    {
        _telecom = telecom;
    }

    public void Execute()
    {
        _telecom.Connect();
    }

    public void Undo()
    {
        _telecom.Disconnect();
    }
}