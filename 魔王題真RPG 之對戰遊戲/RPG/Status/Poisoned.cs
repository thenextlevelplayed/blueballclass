namespace RPG.Status;

public class Poisoned : State
{
    public Poisoned(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 3;

    public override void HandleStartOfTurn()
    {
        if (Role.Duration > 0)
        {
            Role.Hp -= 30;
            if (Role.Hp <= 0)
            {
                this.CanAction = false;
            }
        }
    }

    public override string ToString()
    {
        return "中毒";
    }
}