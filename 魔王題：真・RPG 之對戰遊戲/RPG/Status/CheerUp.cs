namespace RPG.Status;

public class CheerUp : State
{
    public CheerUp(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 3;

    public override void HandleStartOfTurn()
    {
        foreach (var command in Role.Commands)
        {
            command.ActionOption.Str += 50;
        }
    }

    public override void HandleEndOfTurn()
    {
        if (Role.Duration > 0)
        {
            Role.Duration--;
        }
        else
        {
            Role.EnterState(new Normal(Role));
        }

        foreach (var command in Role.Commands)
        {
            command.ActionOption.Str -= 50;
        }
    }

    public override string ToString()
    {
        return "受到鼓舞";
    }
}