namespace 快捷鍵設置機制.Command_Pattern;

public class DisconnectTelecom : ICommand
{
    private readonly Telecom _telecom;

    public DisconnectTelecom(Telecom telecom)
    {
        _telecom = telecom;
    }

    public void Execute()
    {
        _telecom.Disconnect();
    }

    public void Undo()
    {
        _telecom.Connect();
    }
}