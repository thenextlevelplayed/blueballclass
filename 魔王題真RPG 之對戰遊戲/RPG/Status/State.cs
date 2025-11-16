namespace RPG.Status;

public abstract class State
{
    public abstract int InitialDuration { get; set; }

    public virtual bool CanAction { get; protected set; } = true;
    protected Role Role { get; set; }

    protected State(Role role)
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

    public abstract void HandleStartOfTurn();

    public virtual void HandleEndOfTurn()
    {
        if (this is Normal)
        {
            return;
        }

        Role.Duration--;

        if (Role.Duration <= 0)
        {
            ExitState(new Normal(Role));
        }
    }
}