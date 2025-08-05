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

public class Petrochemical : State
{
    public Petrochemical(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 3;
    public override void HandleStartOfTurn()
    {
        this.CanAction = false;
    }
}

public class CheerUp : State
{
    public CheerUp(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 3;
    public override void HandleStartOfTurn()
    {
        Role.Str += 50;
    }
    
    public override void HandleEndOfTurn()
    {
        if (Role.Duration > 0)
        {
            Role.Duration--;
        }
        else
        {
            Role.Str -= 50;
            Role.EnterState(new Normal(Role));
        }
    }
}

public class Poisoned : State
{
    public Poisoned(Role role) : base(role)
    {
    }

    public override int InitialDuration { get; set; } = 3;
    
    public override void HandleStartOfTurn()
    {
        if(Role.Duration > 0){
            Role.Hp -=30;
            if(Role.Hp<=0)
            {
               this.CanAction = false;
            }
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
        this.CanAction = true;
    }
}