namespace 快捷鍵設置機制.Command_Pattern;

public class MoveForwardTank : ICommand
{
    private readonly Tank _tank;

    public MoveForwardTank(Tank tank)
    {
        _tank = tank;
    }

    public void Execute()
    {
        _tank.MoveForward();
    }

    public void Undo()
    {
        _tank.MoveBackward();
    }
}