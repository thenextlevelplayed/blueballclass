namespace RPG.Status;

public class Normal : State
{
    public Normal(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 1;

    public override void HandleStartOfTurn()
    {
        this.CanAction = true;
    }

    public override string ToString()
    {
        return "正常";
    }
}