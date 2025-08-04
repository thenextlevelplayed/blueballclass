namespace RPG.Status;

public abstract class State
{
    public abstract int InitialDuration { get; set; }
    public Role Role { get; set; }

    public State(Role role)
    {
        this.Role = role;
    }

    public void EntryState(State state)
    {
        Role.State = state;
        Role.Duration = state.InitialDuration;
    }

    public void ExitState(State state)
    {
        Role.State = state;
        Role.Duration = 1;
    }

    public virtual bool CanAction()
    {
        return true;
    }

    public abstract void HandleStartOfTurn();

    public void HandleEndOfTurn()
    {
        if (Role.Duration > 0 && this is not Normal)
        {
            Role.Duration--;
        }
        else
        {
            Role.EnterState(new Normal(Role));
        }
    }
}

public class Normal : State
{
    public Normal(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 1;
    public override void HandleStartOfTurn()
    {
        // Normal state does not have any specific start of turn behavior
    }
}