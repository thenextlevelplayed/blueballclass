namespace RPG.Status;

public class Petrochemical : State
{
    public Petrochemical(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 3;

    public override void HandleStartOfTurn()
    {
        if (Role.Duration > 0)
        {
            this.CanAction = false;
        }
        else
        {
            this.CanAction = true;
        }
    }

    public override string ToString()
    {
        return "石化";
    }
}