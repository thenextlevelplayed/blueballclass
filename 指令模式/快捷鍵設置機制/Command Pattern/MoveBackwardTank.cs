namespace 快捷鍵設置機制.Command_Pattern;

public class MoveBackwardTank:ICommand
{
    private readonly Tank _tank;

    public MoveBackwardTank(Tank tank)
    {
        _tank = tank;
    }

    public void Execute()
    {
        _tank.MoveBackward();
    }

    public void Undo()
    {
        _tank.MoveForward();
    }
}