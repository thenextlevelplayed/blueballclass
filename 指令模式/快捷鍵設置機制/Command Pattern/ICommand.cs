namespace 快捷鍵設置機制.Command_Pattern;

public interface ICommand
{
    void Execute();

    void Undo();
}